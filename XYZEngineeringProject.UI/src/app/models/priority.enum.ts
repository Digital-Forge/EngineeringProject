export enum Priority {
    No, 
    Low, 
    Medium, 
    High, 
    // No = 'No', 
    // Low = 'Low', 
    // Medium = 'Medium', 
    // High = 'High', 
    // Done = 'Done'
}

export const Priority2LabelMapping: Record<Priority, string> = {
    [Priority.No]: "No",
    [Priority.Low]: "Low",
    [Priority.Medium]: "Medium",
    [Priority.High]: "High",
};