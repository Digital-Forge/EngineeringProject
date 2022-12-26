import { TranslateService } from '@ngx-translate/core';
import { FileModel } from './../../models/file.model';
import { FileStructure } from './../../models/fileStructure.model';
import { FormBuilder } from '@angular/forms';
import { DocumentService } from './../../services/document/document.service';
import { Observable, ReplaySubject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TypeCheckScopeRegistry } from '@angular/compiler-cli/src/ngtsc/scope';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css']
})
export class DocumentComponent implements OnInit {

  documentForm = this.fb.group({
    name: [''],
    file: [''],
    fileSource: ['']
  })

  directoryForm = this.fb.group({
    directoryName: ['']
  })

  editFileForm = this.fb.group({
    fileId: [''],
    newFileName: ['']
  })

  editDirectoryForm = this.fb.group({
    directoryId: [''],
    newDirectoryName: ['']
  })

  fileInfos: Observable<any> | null = null;
  constructor(
    private documentService: DocumentService,
    private fb: FormBuilder,
    private translateService: TranslateService
  ) {}

  fileStructure: FileStructure = {
    id: '',
    name: '',
    path: '',
    files: [],
    directories: [],
  };

  selectedDirectory: FileStructure = {
    id: '',
    name: '',
    path: '',
    files: [],
    directories: [],
  }

  fileData: FileModel = {
    id: '',
    name: '',
    path: '',
    format: '',
    objectBase64: undefined,
    directoryId: 'BCD2F5AF-265E-4921-2DDB-08DAD2BDDB0D'
  }

  addFile: boolean = false;
  editFile: boolean = false;

  addDirectory: boolean = false;
  editDirectory: boolean = false;

  editingFile: number = 0;

  editingDirectory: number = 0;

  emptyGuid: string = environment.emptyGuid;

  parentDirectoryId: string = '';

  ngOnInit(): void {
    this.documentService.getTreeData().subscribe({
      next: (res) => {
        this.fileStructure = res;
        this.selectedDirectory = this.fileStructure;
      }
    })
  }

  convertFile(file: File): Observable<any> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target!.result!.toString()));
    return result;
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file: File = event.target.files[0];
      this.fileData.name = this.documentForm.controls.name?.value || file.name.split('.')[0]
      this.fileData.format = file.name.split('.')[1];
      this.convertFile(event.target.files[0]).subscribe(base64 => {
        this.fileData.objectBase64 = base64;
      });
    }
  }

  goToDirectory(targetDirectory: FileStructure) {
    this.parentDirectoryId = this.selectedDirectory.id;
    this.selectedDirectory = targetDirectory;
  }

  toggleAddFile(directoryId: string) {
    this.addDirectory = false;

    if (!this.addFile) {
      this.fileData.directoryId = directoryId;
    }
    this.addFile = !this.addFile;
  }

  addFileToDirectory() {
    const formData = new FormData();
    formData.append('file', this.documentForm.get('fileSource')?.value || '');
    this.fileData.id = environment.emptyGuid
    this.documentService.uploadFile(this.fileData).subscribe({
      next: (res) => {
        window.location.reload();
      }
    })
  }

  toggleAddDirectory() {
    this.addFile = false;
    this.addDirectory = !this.addDirectory;
  }

  addDirectoryToDirectory() {
    this.documentService.createNewDirectory(this.selectedDirectory.id, this.directoryForm.controls.directoryName.value || 'test').subscribe({
      next: (res) => {
        window.location.reload();
      }
    })
  }

  goUp() {
    let rootDirectory = this.fileStructure
    this.findParent(rootDirectory, this.selectedDirectory)
  }

  findParent(directory: FileStructure, childDirectory: FileStructure) {
    for (let i = 0; i < directory.directories.length; i++) {
      if (childDirectory.id == directory.directories[i].id) {
        this.selectedDirectory = directory;
      }
      else {
        this.findParent(directory.directories[i], childDirectory);
      }
    }
  }

  editFileName(i: number) {
    this.editingFile = i;
    this.editFile = !this.editFile;
  }

  cancelEditFileName(i: number) {
    this.editFile = false;
  }

  changeFileName(id: string) {
    this.editFileForm.controls.fileId.setValue(id);
    this.editFile = false;
    if (this.editFileForm.controls.fileId.value && this.editFileForm.controls.newFileName.value) {
      this.documentService.changeFileName(this.editFileForm.controls.fileId.value, this.editFileForm.controls.newFileName.value).subscribe({
        next: (res) => {
          window.location.reload();
        }
      });
    }
  }

  editDirectoryName(i: number) {
    this.editingDirectory = i;
    this.editDirectory = !this.editDirectory;
  }

  cancelEditDirectoryName(i: number) {
    this.editDirectory = false;
  }

  changeDirectoryName(id: string) {
    this.editDirectoryForm.controls.directoryId.setValue(id);
    this.editDirectory = false;
    if (this.editDirectoryForm.controls.directoryId.value && this.editDirectoryForm.controls.newDirectoryName.value) {
      this.documentService.changeDirectoryName(this.editDirectoryForm.controls.directoryId.value, this.editDirectoryForm.controls.newDirectoryName.value).subscribe({
        next: (res) => {
          window.location.reload();
        }
      })
    }
  }

  deleteFile(id: string) {
    if (confirm(this.translateService.instant('Alert.deleteFile'))) {
      this.documentService.deleteFile(id).subscribe({
        next: (res) => {
          window.location.reload();
        }
      });
    }
  }

  deleteDirectory(id: string) {
    if (confirm(this.translateService.instant('Alert.deleteDirectory'))) {
      this.documentService.deleteDirectory(id).subscribe({
        next: (res) => {
          window.location.reload();
        }
      })
    }
  }
}
