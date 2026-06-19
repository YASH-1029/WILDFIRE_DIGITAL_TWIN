import pandas as pd
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import (
    accuracy_score,
    precision_score,
    recall_score,
    f1_score,
    confusion_matrix,
    classification_report
)
import joblib

# Load dataset
df = pd.read_csv("dataset_algerian.csv")

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

# Input features
X = df[[
    "Temperature",
    "RH",
    "Ws",
    "Rain"
]]

# Output label
y = df["Classes"]

# Train-Test Split
X_train, X_test, y_train, y_test = train_test_split(
    X,
    y,
    test_size=0.2,
    random_state=42
)

# Create Random Forest model
model = RandomForestClassifier(
    n_estimators=100,
    random_state=42
)

# Train model
model.fit(X_train, y_train)

# Predictions
y_pred = model.predict(X_test)

# Metrics
accuracy = accuracy_score(y_test, y_pred)
precision = precision_score(y_test, y_pred)
recall = recall_score(y_test, y_pred)
f1 = f1_score(y_test, y_pred)
cm = confusion_matrix(y_test, y_pred)

# Print results
print("Random Forest Results")
print("---------------------")
print("Accuracy  =", round(accuracy, 4))
print("Precision =", round(precision, 4))
print("Recall    =", round(recall, 4))
print("F1 Score  =", round(f1, 4))

print("\nConfusion Matrix")
print(cm)

print("\nClassification Report")
print(classification_report(y_test, y_pred))

# Save model
joblib.dump(model, "wildfire_model.pkl")

print("\nModel saved successfully!")