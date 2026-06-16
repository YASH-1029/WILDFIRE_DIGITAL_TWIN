import sys
import os
import pandas as pd
import joblib

current_dir = os.path.dirname(os.path.abspath(__file__))

model_path = os.path.join(current_dir, "wildfire_model.pkl")

model = joblib.load(model_path)

temperature = float(sys.argv[1])
humidity = float(sys.argv[2])
wind_speed = float(sys.argv[3])
rain = float(sys.argv[4])

input_data = pd.DataFrame({
    "Temperature": [temperature],
    "RH": [humidity],
    "Ws": [wind_speed],
    "Rain": [rain]
})

prediction = model.predict(input_data)

if prediction[0] == 1:
    print("FIRE")
else:
    print("NOT_FIRE")