import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent implements OnInit{
   
  public usuarios: usuario[];
    nuevoUsuario: usuario;
    httpClient: HttpClient;
    usuariosUrl: string;
    contactForm: FormGroup;
   
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private formBuilder: FormBuilder) {
        this.httpClient = http;
        this.usuariosUrl = baseUrl + 'api/SampleData/';
        this.createContactForm();
      }
    ngOnInit(): void {
        this.GetUsuarios();
    }

    createContactForm() {
        this.contactForm = this.formBuilder.group({
            nombre: [''],
            email: ['']
        });
    }

    onSubmit() {
        //console.log("Aqui recibo");
        //console.log(this.contactForm.value.nombre);
        let nuevoUsuario: usuario = new usuario();
        nuevoUsuario.nombre = this.contactForm.value.nombre;
        nuevoUsuario.email = this.contactForm.value.email;
       
        return this.httpClient.post<usuario>(this.usuariosUrl + 'PostUsuario', nuevoUsuario)
            .subscribe(
                (val) => {
                    console.log("POST call successful value returned in body",
                        val);
                    this.GetUsuarios();
                },
                response => {
                    console.log("POST call in error", response);
                },
                () => {
                    console.log("The POST observable is now completed.");
                });
        //alert('Mensaje Enviado !');
    }

    GetUsuarios() {
        this.httpClient.get<usuario[]>(this.usuariosUrl + 'GetUsuarios').subscribe(result => {
            this.usuarios = result;
        }, error => console.error(error));

    }
}

class usuario {
    id: number;
  nombre: string;
  email: string;
}
