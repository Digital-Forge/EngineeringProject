export interface FileModel {
    id: string,
    name: string,
    format: string,
    path: string,
    objectBase64?: string
    directoryId: string
}