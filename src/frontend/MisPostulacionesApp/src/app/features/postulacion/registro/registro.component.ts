import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PostulacionService } from '../../../core/services/postulacion.service';

// Importaciones de Angular Material
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

  // FORM SOLO PARA IA
  formIA: FormGroup;

  // FORM SOLO PARA GUARDAR POSTULACIÓN
  form: FormGroup;

  procesando = false;
  guardando = false;

  constructor(
    private fb: FormBuilder,
    private postulacionService: PostulacionService,
    private snackBar: MatSnackBar
  ) {

    // ✅ FORM IA
    this.formIA = this.fb.group({
      textoIA: ['', Validators.required]
    });

    // ✅ FORM POSTULACIÓN
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
    
    // Crear objeto con la estructura correcta para el backend
    const postulacionData = {
      titulo: this.form.get('titulo')?.value,
      empresa: this.form.get('empresa')?.value,
      rol: this.form.get('rol')?.value,
      descripcion: this.form.get('descripcion')?.value,
      tecnologias: this.form.get('tecnologias')?.value,
      salario: this.form.get('salario')?.value,
      modalidad: this.form.get('modalidad')?.value,
      plataforma: this.form.get('plataforma')?.value,
      notas: this.form.get('notas')?.value
    };

    this.postulacionService.registrarPostulacion(postulacionData).subscribe({
      next: (response) => {
        console.log('Postulación guardada:', response);
        this.guardando = false;
        this.mostrarExito('¡Postulación guardada exitosamente!');
        this.limpiarFormulario();
      },
      error: (err) => {
        console.error('Error al guardar postulación:', err);
        this.guardando = false;
        this.mostrarError('Error al guardar la postulación. Intenta nuevamente.');
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