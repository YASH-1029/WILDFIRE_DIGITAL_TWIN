import joblib
import pandas as pd

# Load trained model
model = joblib.load("wildfire_model.pkl")

# Example sensor values
temperature = 28
humidity = 80
wind_speed = 8
rain = 5

# Create input dataframe
input_data = pd.DataFrame({
    "Temperature": [temperature],
    "RH": [humidity],
    "Ws": [wind_speed],
    "Rain": [rain]
})

# Predict
prediction = model.predict(input_data)

if prediction[0] == 1:
    print("Prediction : FIRE")
else:
    print("Prediction : NOT FIRE")