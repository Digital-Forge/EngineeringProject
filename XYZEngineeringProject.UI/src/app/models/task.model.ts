import { TaskListStatus } from './taskListStatus.enum';
import { Priority } from "./priority.enum";

export interface Task {
    id: string,
    deadline: Date,
    priority: Priority,
    title: string,
    description?: string,
    assigneeUserId?: string,
    assignerUserId:string,
    listOfTasksId?: string,
    createDate: Date,
    isComplete: boolean 
}

export interface TaskList {
    id: string,
    name: string,
    createDate: Date,
    project?: string,
    status: TaskListStatus,
    tasks?: Task[]
}

export interface TaskListResponse {
    taskListData: TaskList,
    tasks?: Task[],
    isListComplete?: boolean
}