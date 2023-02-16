#Izabela Zelek February 2023
#Based on Neural Network by Oisin Cawley, March 2021

import numpy as np
from numpy import loadtxt
from numpy import mean
from numpy import std
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Dense
from sklearn.model_selection import RepeatedKFold

#Load the dataset
#The 6 inputs to the Olympus Runs Neural Network are:
#1) X-Coordinate of the Obstacle
#2) Y-Coordinate of the Obstacle
#3) Z-Coordinate of the Obstacle
#4) X-Coordinate of the Runner
#5) Y-Coordinate of the Runner
#6) Y-Coordinate of the Runner

dataset = loadtxt('training_data.csv', delimiter=',')

# split into input (X) and output (y) variables
X = dataset[:,0:6]
y = dataset[:,6]

results = list()

# define evaluation procedure
cv = RepeatedKFold(n_splits=10, n_repeats=3, random_state=1)
#enumerate folds
for train_ix, test_ix in cv.split(X):
#prepare data
	X_train, X_test = X[train_ix], X[test_ix]
	y_train, y_test = y[train_ix], y[test_ix]
	#define model
	model = Sequential()
	model.add(Dense(5,input_dim=6,kernel_initializer='he_uniform',activation='relu'))

	model.add(Dense(3))

	model.compile(loss='mae', optimizer='adam')
	#fit model
	model.fit(X_train, y_train, verbose=0, epochs=100)
	#evaluate model on test set
	mae = model.evaluate(X_test,y_test, verbose = 0)

	print('Accuracy: %.2f' % mae)
	results.append(mae)

print('MAE: %.3f (%.3f)' % (mean(results), std(results)))
	
# save model and architecture to single file
model.save("olympus_model.h5")

print("Saved model to disk")


# # define the keras model. The same as the structure we use in the Runner's brain.
# model = Sequential()
# model.add(Dense(3,input_dim=15, activation='softmax'))

# # compile the keras model
# model.compile(loss='category_crossentropy', optimizer='adam', metrics=['accuracy'])

# # fit the keras model on the dataset
# model.fit(X, y, epochs=100, batch_size=100)

# # evaluate the keras model
# _, accuracy = model.evaluate(X, y)
# print('Accuracy: %.2f' % (accuracy*100))

# # make some class predictions with the model as a demonstration
# predictions = np.argmax(model.predict(X), axis=-1)
# # summarize the first 15 cases
# for i in range(15):
# 	print('%s => %d (expected %d)' % (X[i].tolist(), predictions[i], y[i]))
	
# # save model and architecture to single file
# model.save("olympus_model.h5")

# print("Saved model to disk")
