import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PostulacionService } from '../../../core/services/postulacion.service';
import { MensajeService } from '../../../core/services/mensaje/mensaje.service';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatSnackBarModule
  ],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.css'
})
export class RegistroComponent {

  
  formIA: FormGroup;
  form: FormGroup;
  procesando = false;
  guardando = false;

  constructor(
    private fb: FormBuilder,
    private postulacionService: PostulacionService,
    private mensajeService: MensajeService,
    private snackBar: MatSnackBar
  ) {

    
    this.formIA = this.fb.group({
      textoIA: ['', Validators.required]
    });

    
    this.form = this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(3)]],
      empresa: ['', [Validators.required, Validators.minLength(2)]],
      rol: ['', Validators.required],
      descripcion: ['', [Validators.required, Validators.minLength(10)]],
      tecnologias: ['', Validators.required],
      salario: [null],
      modalidad: [''],
      plataforma: [''],
      notas: ['']
    });
  }

  
procesarIA() {
  const texto = this.formIA.get('textoIA')?.value?.trim();

  if (!texto) {
    this.mostrarError('Por favor, ingresa el texto de la oferta');
    return;
  }

  this.procesando = true;

  this.postulacionService.procesarIA(texto).subscribe({
    next: (data) => {
      this.form.patchValue({
        titulo: data.titulo ?? '',
        empresa: data.empresa ?? '',
        rol: data.rol ?? '',
        descripcion: data.descripcion ?? '',
        tecnologias: data.tecnologias ?? '',
        salario: data.salario ?? null,
        modalidad: data.modalidad ?? '',
        plataforma: data.plataforma ?? '',
        notas: data.notas ?? ''
      });

      this.procesando = false;
      this.mostrarExito('¡Información extraída exitosamente!');
    },
    error: () => {
      this.procesando = false;
      this.mostrarError('Error al procesar con IA');
    }
  });
}


  guardarPostulacion() {
  if (this.form.invalid) {
    this.form.markAllAsTouched();
    this.mostrarError('Por favor, completa los campos requeridos');
    return;
  }

  this.guardando = true;

  const postulacionData = {
    titulo: this.form.value.titulo,
    empresa: this.form.value.empresa,
    rol: this.form.value.rol,
    descripcion: this.form.value.descripcion,
    tecnologias: this.form.value.tecnologias,
    salario: this.form.value.salario,
    modalidad: this.form.value.modalidad,
    plataforma: this.form.value.plataforma,
    notas: this.form.value.notas
  };

  this.postulacionService.registrarPostulacion(postulacionData).subscribe({
    next: (response) => {
      this.guardando = false;

      const postulacionId = response.value; 

      this.mostrarExito('¡Postulación guardada exitosamente!');
      this.enviarMensaje(postulacionId);

      this.limpiarFormulario();
    },
    error: () => {
      this.guardando = false;
      this.mostrarError('Error al guardar la postulación');
    }
  });
}

    private enviarMensaje(postulacionId: string) {
    const payload = {
      numeroDestino: '51943787437',
      id: postulacionId
    };

    this.mensajeService.enviarMensaje(payload).subscribe({
      next: () => {
        this.mostrarExito('Mensaje enviado correctamente');
      },
      error: () => {
        this.mostrarError('La postulación se guardó, pero el mensaje falló');
      }
    });
  }

  limpiarTextoIA() {
    this.form.get('textoIA')?.setValue('');
  }

  limpiarFormulario() {
    this.form.reset({
      textoIA: '',
      titulo: '',
      empresa: '',
      rol: '',
      descripcion: '',
      tecnologias: '',
      salario: null,
      modalidad: '',
      plataforma: '',
      notas: ''
    });
    this.form.markAsPristine();
    this.form.markAsUntouched();
  }

  
  private mostrarExito(mensaje: string) {
    this.snackBar.open(mensaje, 'Cerrar', {
      duration: 3000,
      panelClass: ['snackbar-success']
    });
  }

  private mostrarError(mensaje: string) {
    this.snackBar.open(mensaje, 'Cerrar', {
      duration: 5000,
      panelClass: ['snackbar-error']
    });
  }

  
  get titulo() { return this.form.get('titulo'); }
  get empresa() { return this.form.get('empresa'); }
  get rol() { return this.form.get('rol'); }
  get descripcion() { return this.form.get('descripcion'); }
  get tecnologias() { return this.form.get('tecnologias'); }
}