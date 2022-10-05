import { Priority } from "./priority.enum";
import { Client } from "./client.model";

export interface Task {
    id: string,
    deadline?: Date,
    priority: Priority, // UWAGA dałoby radę zmienić to na priorityId z odniesieniem do tabeli taskPriority (none, low, medium, high)?
    title: string,
    description?: string,
    assignedUserId?: string,
    taskListId?: string,
    isComplete?: boolean 
    //TODO te pola ostatecznie będą obowiązkowe, więc trzeba będzie dorobić je w componencie
}

export interface TaskList {
    id: string,
    name: string,
    dateCreate: Date,
    project: string, //krótki, 100 znaków
    client: Client,
    status: string, //odniesienie do task list status (new, in_progress, complete)
}