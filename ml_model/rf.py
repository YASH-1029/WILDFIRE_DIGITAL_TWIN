import pandas as pd
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score
import joblib

# Load dataset
df = pd.read_csv( "dataset_algerian.csv")


# Remove spaces from column names
df.columns = df.columns.str.strip()


# Remove spaces from Classes values
df["Classes"] = df["Classes"].str.strip()

# Convert output labels
df["Classes"] = df["Classes"].replace({
    "not fire": 0,
    "fire": 1
})

# Remove date columns
df = df.drop(columns=["day", "month", "year"])

# Inputs
X = df[[
    "Temperature",
    "RH",
    "Ws",
    "Rain"
]]

# Output
y = df["Classes"]

# Split dataset
X_train, X_test, y_train, y_test = train_test_split(
    X,
    y,
    test_size=0.2,
    random_state=42
)

# Create model
model = RandomForestClassifier(
    n_estimators=100,
    random_state=42
)

# Train
model.fit(X_train, y_train)

# Test
y_pred = model.predict(X_test)

# Accuracy
accuracy = accuracy_score(y_test, y_pred)

print("Accuracy =", accuracy)

# Save model
joblib.dump(model, "wildfire_model.pkl")

print("Model saved successfully!")