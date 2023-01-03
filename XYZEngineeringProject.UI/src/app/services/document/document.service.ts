import { FileModel } from './../../models/file.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private baseUrl = environment.baseApiUrl;

  constructor(
    private httpClient:HttpClient
  ) { }

  upload(formData: FormData): Observable<any> {
    return this.httpClient.post(this.baseUrl+'File/Upload',formData);
  }

  uploadFile(fileData: FileModel): Observable<any> {
    return this.httpClient.post(this.baseUrl+'File/UploadFile',fileData);
  }

  getTreeData(): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}File/GetTreeData`);
  }

  createDefaultDirectory(): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}File/CreateDefaultDirectory`)
  }

  createNewDirectory(parentId: string, name: string): Observable<any> {
    return this.httpClient.get(this.baseUrl+'File/CreateDirectory/'+parentId+'/'+name);
  }

  changeFileName(fileId:string, name:string): Observable<any> {
    return this.httpClient.get(this.baseUrl+'File/EditFileName/'+fileId+'/'+name);
  }

  changeDirectoryName(directoryId:string, name:string): Observable<any> {
    return this.httpClient.get(this.baseUrl+'File/EditDirectoryName/'+directoryId+'/'+name);
  }

  deleteFile(id: string): Observable<any> {
    return this.httpClient.get(this.baseUrl+'File/DeleteFile/' + id);
  }

  deleteDirectory(id:string){
    return this.httpClient.get(this.baseUrl+'File/DeleteDirectory/' + id);
  }

  downloadFile(id: string): Observable<FileModel> {
    return this.httpClient.get<FileModel>(this.baseUrl+'File/Download/'+id);
  }
}
