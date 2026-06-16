print("\n==============================")
print("   WILDFIRE MODEL COMPARISON")
print("==============================")

# Model accuracies
lr_accuracy = 0.7551
dt_accuracy = 0.7755
rf_accuracy = 0.7959
svm_accuracy = 0.6531

print(f"\nLogistic Regression : {lr_accuracy*100:.2f}%")
print(f"Decision Tree       : {dt_accuracy*100:.2f}%")
print(f"Random Forest       : {rf_accuracy*100:.2f}%")
print(f"SVM                 : {svm_accuracy*100:.2f}%")

# Find best model
models = {
    "Logistic Regression": lr_accuracy,
    "Decision Tree": dt_accuracy,
    "Random Forest": rf_accuracy,
    "SVM": svm_accuracy
}

best_model = max(models, key=models.get)

print("\n------------------------------")
print("BEST MODEL")
print("------------------------------")
print(best_model)

