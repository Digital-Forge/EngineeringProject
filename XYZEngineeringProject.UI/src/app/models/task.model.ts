import { TaskListStatus } from './taskListStatus.enum';
import { Priority } from "./priority.enum";
import { Client } from "./client.model";

export interface Task {
    id: string,
    deadline?: Date,
    priority: Priority, // UWAGA dałoby radę zmienić to na priorityId z odniesieniem do tabeli taskPriority (none, low, medium, high)?
    title?: string,
    description?: string,
    assigneeUserId?: string,
    assignerUserId:string,
    listOfTasksId?: string,
    createDate:Date,
    isComplete: boolean 
    //TODO te pola ostatecznie będą obowiązkowe, więc trzeba będzie dorobić je w componencie
}

export interface TaskList {
    id: string,
    name: string,
    createDate?: Date,
    project?: string, //krótki, 100 znaków
    //client: Client,
    status: TaskListStatus, //odniesienie do task list status (new, in_progress, complete)
    tasks?: Task[]
}

export interface TaskListResponse {
    taskListData: TaskList,
    tasks?: Task[],
    isComplete?: boolean
}