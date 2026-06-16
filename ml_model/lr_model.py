import pandas as pd
from sklearn.linear_model import LogisticRegression
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score, confusion_matrix

# Load dataset
df = pd.read_csv("dataset_algerian.csv")

# Clean column names
df.columns = df.columns.str.strip()

# Clean class labels
df["Classes"] = df["Classes"].str.strip()

# Convert labels
df["Classes"] = df["Classes"].replace({
    "not fire": 0,
    "fire": 1
})

# Features
X = df[[
    "Temperature",
    "RH",
    "Ws",
    "Rain"
]]

# Target
y = df["Classes"]

# Train-test split
X_train, X_test, y_train, y_test = train_test_split(
    X, y,
    test_size=0.2,
    random_state=42
)

# Model
model = LogisticRegression(max_iter=1000)

# Train
model.fit(X_train, y_train)

# Predict
y_pred = model.predict(X_test)

# Accuracy
accuracy = accuracy_score(y_test, y_pred)

print("Logistic Regression Accuracy =", round(accuracy,4))

# Confusion Matrix
print(confusion_matrix(y_test, y_pred))