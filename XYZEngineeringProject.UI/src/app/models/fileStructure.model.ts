import { FileModel } from './file.model';
export interface FileStructure {
    id: string,
    name: string,
    path: string
    files: FileModel[],
    directories: FileStructure[]
}
export class FileStructure {
    id: string;
    name: string;
    path: string;
    files: FileModel[];
    directories: FileStructure[]
}