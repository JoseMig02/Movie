<template>
         <q-btn @click="openModal" label="Add Movie" color="primary" />

      <!-- Modal Trigger Button -->

      <!-- Modal with Form -->
      <q-dialog v-model="isModalOpen">
        <q-card  style="min-width: 600px; padding: 20px;">
          <q-card-section>
            <q-form @submit.prevent="submitForm">
              <!-- Form Controls -->
              <q-input 
              :rules="[val => !!val || 'required']" v-model="movie.titulo" label="Title" />
              
              <q-select :rules="[val => !!val || 'required']"
                v-model="movie.genero"
                :options="genres"
                label="Genre"
                option-label="label"
                option-value="value"
              />
  
              <q-select :rules="[val => !!val || 'required']"
                v-model="movie.clasificacion"
                :options="ratings"
                label="Rating"
                option-label="label"
                option-value="value"
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="actorInput"
                label="Actors"
                @keyup.enter="addActor"
              />
              <q-chip 
                v-for="(actor, index) in movie.actores"
                :key="index"
                removable
                @remove="removeActor(index)"
              >
                {{ actor }}
              </q-chip>
  
              <q-toggle
                v-model="movie.enCartelera"
                label="In Theaters"
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.fechaEstreno"
                type="date"
                label="Release Date"
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.imagenUrl"
                label="Image URL"
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.sinopsis"
                type="textarea"
                label="Synopsis"
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.trailerUrl"
                label="Trailer URL"
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.audioPromocionalUrl"
                label="Promotional Audio URL"
              />
  
              <q-btn type="submit" label="Submit" color="primary" />
              <q-btn @click="closeModal" label="Cancel" color="secondary" flat />
            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>

  </template>
  
  <script setup>
  import { ref } from 'vue';
  import { createMovie } from '../API/Movie'; // Asegúrate de la ruta correcta
  
  const isModalOpen = ref(false);
  
  const movie = ref({
    titulo: '',
    genero: null,
    clasificacion: null,
    actores: [],
    enCartelera: false,
    fechaEstreno: '',
    imagenUrl: '',
    sinopsis: '',
    trailerUrl: '',
    audioPromocionalUrl: ''
  });
  
  const actorInput = ref('');
  
  const genres = ref([
    { label: 'Action', value: 'action' },
    { label: 'Comedy', value: 'comedy' },
    { label: 'Drama', value: 'drama' }
  ]);
  
  const ratings = ref([
    { label: 'G', value: 'g' },
    { label: 'PG', value: 'pg' },
    { label: 'PG-13', value: 'pg13' },
    { label: 'R', value: 'r' }
  ]);
  
  const openModal = () => {
    isModalOpen.value = true;
  };
  
  const closeModal = () => {
    isModalOpen.value = false;
  };
  
  const submitForm = async () => {
    try {
      await createMovie({
        ...movie.value,
        genero: movie.value.genero?.value, // Send only value
        clasificacion: movie.value.clasificacion?.value // Send only value
      });
     
      closeModal();
      emit('add-movie');
    } catch (error) {
      console.error('Error submitting form:', error);
    }
  };
  
  const addActor = () => {
    if (actorInput.value.trim() !== '') {
      movie.value.actores.push(actorInput.value.trim());
      actorInput.value = '';
    }
  };
  
  const removeActor = (index) => {
    movie.value.actores.splice(index, 1);
  };
  </script>
  
  