export enum Priority {
    No, 
    Low, 
    Medium, 
    High, 
    Done
    // No = 'No', 
    // Low = 'Low', 
    // Medium = 'Medium', 
    // High = 'High', 
    // Done = 'Done'
}

export const Priority2LabelMapping: Record<Priority, String> = {
    [Priority.No]: "No",
    [Priority.Low]: "Low",
    [Priority.Medium]: "Medium",
    [Priority.High]: "High",
    [Priority.Done]: "Done"
};