// Coders:
// Caroline Percy
// ...

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Brain of the AI Runner.
/// </summary>

public class AI_Brain : MonoBehaviour
{
    public int numInputs = 6;
    public int numHidden = 5;
    public int numOutputs = 1;

    public Transform parent;

    private float[,] _weightsLayer1;  // From inputs to hidden layer
    private float[,] _weightsLayer2;   // From hidden layer to output neuron
    private float[] _biases;

    private bool _useBiases = false;	// If set to false no Bias values will be used.
    //private CSVReader _csvReader;

    /// The Rigidbody of the Runner - Used to apply the force for the jump.
    Rigidbody rb;

    /// The amount of force applied to the Runner in order to get them off the ground.
    float jumpForce = 80.0f;

    /// Bool that keeps track of whether the Runner is on the ground or not.
    bool grounded = false;

    [Header("AI_Sliding")]
    /// Maximum slide time 
    public float maxSlideTime = 1.0f;
    /// How much force is applied to gameobject when sliding  
    public float slideForce;
    /// Timer to check how long the gameobject is sliding for before going back to running 
    private float slideTimer;

    /// The scale will be half of what the gameobject is to show that it is currently sliding 
    public float slideYScale;
    /// The scale will be double of what the gameobject is to show that it is currently sliding
    public float slideZScale;

    ///The current scale of the AI Runner.
    private Vector3 AIScale;

    ///
    public Transform AIObj;

    /// How much the runner moves when changing lanes
    private int laneSize = 2;

    /// max amount of lanes
    private int laneCount = 3;

    /// current lane occupied by runner
    private int currentLane = 2;

    /// Keeps track of whether the AI runner is currently sliding.
    bool _sliding;
    /// Public reference to _sliding.
    public bool sliding { get { return _sliding; } }


    /// <summary>
    /// A function that is called at the start of the game, to get the components in the Runner.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        AIScale = AIObj.localScale;

        Init(6, 5, 3);
        //_csvReader = GetComponent<CSVReader>();
        //_csvReader.ReadCSV();
    }

    //void Update()
    //{
    //    float idk = -10;
    //    float[] input = {parent.GetChild(0).position.x, parent.GetChild(0).position.y, parent.GetChild(0).position.z,
    //    transform.position.x,transform.position.y,transform.position.z};
    //    idk = FeedForward(input);

    //    Debug.Log(idk);
    //    //if (idk == 1)
    //    //{
    //    //    transform.position = new Vector3(2, transform.position.y, transform.position.z);

    //    //}
    //    //if(idk == 0)
    //    //{
    //    //    transform.position = new Vector3(0, transform.position.y, transform.position.z);
    //    //}
    //}

    // Start is called before the first frame update
    public void Init(int inputs, int hidden, int outputs)
    {
        numInputs = inputs;
        numHidden = hidden;
        numOutputs = outputs;

        _weightsLayer1 = new float[numInputs, numHidden];

        /// Initialse weights for first layer to random values between -1 and 1.
        for (int i = 0; i < numInputs; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                _weightsLayer1[i, j] = Random.Range(-1.0f, 1.0f); ;
            }
        }

        _weightsLayer2 = new float[numHidden, numOutputs];

        /// Initialse weights for second layer to random values between -1 and 1.
        for (int i = 0; i < numHidden; i++)
        {
            for (int j = 0; j < numOutputs; j++)
            {
                _weightsLayer2[i, j] = Random.Range(-1.0f, 1.0f);
            }
        }

        _biases = new float[numHidden + 1];

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

        //SoftMax(soft, numHidden);

        //product = 0.0f;
        //for(int i = 0; i < numHidden; i++)
        //{
        //    product = soft[i] * +_weightsLayer2[i];
        //    output += product;
        //}

        if (_useBiases)
        {
            output += _biases[4];
        }
        //output = Sigmoid(output);

        //if (output < 0.001)
        //{
        //    output = 0;
        //}
        //else
        //if (output > 0.005)
        //{
        //    output = 1;
        //}
        //else
        //{
        //    output = 0;
        //}
        return output;
    }

    // Rectified Linear Unit activation Function
    float ReLu(float val)
    {
        if (val < 0) val = 0;
        return val;
    }

    // Softmax or averaging the value
    void SoftMax(float data, int len)
    {

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

    public void SetWeightsLayer2(int i, int j, float weight)
    {
        _weightsLayer2[i, j] = weight;
    }

    public void SetBias(int i, float weight)
    {
        _biases[i] = weight;
    }

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
        }

    }

    /// <summary>
    /// Controller of how the AI Runner reacts, once it sees an obstacle.
    /// </summary>
    /// <param name="t_seenObstacle">The obstacle it sees ahead.</param>
    public void React(Collider t_seenObstacle)
    {
        bool solved = false;

        if (t_seenObstacle.CompareTag("Inpenetrable"))
        {
            ///If in the middle lane, randomise left or right
            float lane = laneCount / 2.0f;
            if (currentLane == Mathf.Ceil(lane))
            {
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentLane--;
                    solved = true;
                }
                else if (rand == 2)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentLane++;
                    solved = true;
                }
            }
            else if (currentLane > 1 && !solved)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                currentLane--;
                solved = true;
            }
            else if (currentLane < laneCount && !solved)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                currentLane++;
                solved = true;
            }
        }

        if (t_seenObstacle.CompareTag("JumpObstacle") && !solved)
        {
            Jump();
            solved = true;
        }


        if (t_seenObstacle.CompareTag("SlideObstacle") && !solved)
        {
            StartSlide();
            solved = true;
        }
    }

    /// <summary>
    /// A check to see whether the Runner has landed back on the ground.
    /// </summary>
    /// <param name="collision">The object the Runner has collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Land();
        }
    }

    /// <summary>
    /// What happens when the Runner is on the ground again.
    /// </summary>
    void Land()
    {
        grounded = true;
    }

    /// <summary>
    /// The physical motion for the runner to jump.
    /// </summary>
    void Jump()
    {
        if (grounded)
        {
            rb.AddForce(transform.up * jumpForce * 5);
            grounded = false;
        }
    }

    /// <summary>
    /// Start the AI sliding.
    /// </summary>
    private void StartSlide()
    {
        _sliding = true;

        AIObj.localScale = new Vector3(AIObj.localScale.x, slideYScale, slideZScale);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    /// <summary>
    /// End the AI sliding.
    /// </summary>
    private void StopSlide()
    {
        _sliding = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AIObj.localScale = AIScale;
    }


    /// <summary>
    /// Calls every frame to update the brain.
    /// </summary>
    private void Update()
    {
        if (_sliding && slideTimer > 0)
        {
            slideTimer -= Time.deltaTime;
        }
        else
        {
            StopSlide();
        }
    }
}
