import { Department } from './department.model';
import { NoteStatus } from "./noteStatus.enum";

export interface Note {
    id: string,
    title: string,
    // isCompany: boolean,
    // noteStatus: string | null,
    noteStatus: NoteStatus,
    date: Date
    createdBy: string
}