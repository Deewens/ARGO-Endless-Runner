using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    private int numInputs = 6;
    private int numHidden = 5;
    private int numOutputs = 3;

    public Transform parent;

    private float[,] _weightsLayer1;  // From inputs to hidden layer
    private float[] _weightsLayer2;   // From hidden layer to output neuron
    private float[] _biases;

    private bool _useBiases = false;	// If set to false no Bias values will be used.

    private CSVReader _csvReader;

    private void Update()
    {
        float idk = -10;
        //float[] input = {parent.GetChild(0).position.x, parent.GetChild(0).position.y, parent.GetChild(0).position.z,
        //transform.position.x,transform.position.y,transform.position.z};
        //idk = FeedForward(input);

        //Debug.Log(idk);

        //if (idk == 1)
        //{
        //    transform.position = new Vector3(2, transform.position.y, transform.position.z);

        //}
        //if(idk == 0)
        //{
        //    transform.position = new Vector3(0, transform.position.y, transform.position.z);
        //}
    }

    private void Start()
    {
        Init();
        _csvReader = GetComponent<CSVReader>();
        _csvReader.ReadCSV();
    }

    // Start is called before the first frame update
    public void Init()
    {
        _weightsLayer1 = new float[numInputs, numHidden];
        _weightsLayer2 = new float[numHidden];
        _biases = new float[numHidden + 1];

        /// Initialse weights for first layer to random values between -1 and 1.
        for (int i = 0; i < numInputs; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                _weightsLayer1[i,j] = Random.Range(-1.0f, 1.0f);
            }        
        }

        /// Initialse weights for second layer to random values between -1 and 1.
        for (int i = 0; i < numHidden; i++)
        {
            _weightsLayer2[i] = Random.Range(-1.0f, 1.0f);

        }

        /// Last value is the bias for the ouput neuron.
        for (int j = 0; j < numHidden + 1; j++)
        {
            _biases[j] = Random.Range(-1.0f, 1.0f);
        }
    }

    /// <summary>
    /// Evaluates inputs on the network
    /// </summary>
    /// <returns></returns>
    public float FeedForward(float[] inputs)
    {
        float output = 0.0f;

        float[] dot = new float[5];
        float[] soft = new float[5];

        float product = 0.0f;

        for (int i = 0; i < 5; i++)
        {
            dot[i] = 0.0f;
            soft[i] = 0.0f;
        }

        ///Applies weights to inputs for hidden layer
        for (int i = 0; i < numHidden; i++)
        {
            product = 0.0f;
            for (int j = 0; j < numInputs; j++)
            {
                product = inputs[j] * _weightsLayer1[j, i];
                dot[i] += product;
            }
            if (_useBiases)
            {
                dot[i] += _biases[i];
            }
            soft[i] = ReLu(dot[i]);
        }

        SoftMax(soft, numHidden);

        product = 0.0f;

        for (int i = 0; i < numHidden; i++)
        {
            product = soft[i] * +_weightsLayer2[i];
            output += product;
        }

        if (_useBiases)
        {
            output += _biases[4];
        }

        output = Sigmoid(output);

        if (output > 0.5)
            return 1;
        else
            return 0;
    }

    // Rectified Linear Unit activation Function
    float ReLu(float val)
    {
        if (val < 0) val = 0;
        return val;
    }

    // Softmax or averaging the value
    void SoftMax(float[] data, int len)
    {
        float sum = 0;
        for (int i = 0; i < len; i++)
        {
            sum = sum + data[i];
        }
        if (sum == 0) sum = 1;
        for (int i = 0; i < len; i++)
        {
            data[i] = data[i] / sum;
            //		std::cout << "result of softmax for node " << i << " = " << data[i] << std::endl;
        }
    }

    // Sigmoid Activation Function
    float Sigmoid(float z)
    {
        return 1.0f / (1.0f + Mathf.Exp(-z));
    }

    public void SetWeightsLayer1(int i, int j, float weight)
    {
        _weightsLayer1[i, j] = weight;
    }

    public void SetWeightsLayer2(int i, float weight)
    {
        _weightsLayer2[i] = weight;
    }

    public void SetBias(int i, float weight)
    {
        _biases[i] = weight;
    }
}
