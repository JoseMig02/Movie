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
              :rules="[val => !!val || 'required']" v-model="movie.titulo" label="Title"  outlined rounded />
              
              <q-select :rules="[val => !!val || 'required']"
                v-model="movie.genero"
                :options="genres"
                label="Genre"
                option-label="label"
                option-value="value"
                outlined rounded
              />
  
              <q-select :rules="[val => !!val || 'required']"
                v-model="movie.clasificacion"
                :options="ratings"
                label="Rating"
                option-label="label"
                option-value="value"
                outlined rounded
              />
  
              <q-toggle
                v-model="movie.enCartelera"
                label="In Theaters"
              />
   
               <q-input :rules="[val => !!val || 'required']"
                v-model="movie.fechaEstreno"
                type="date"
                label="Release Date"
                outlined rounded
              />
               
              
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.imagenUrl"
                label="Image URL"
                outlined rounded
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.sinopsis"
                type="textarea"
                label="Synopsis"
                outlined rounded
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.trailerUrl"
                label="Trailer URL"
                outlined rounded
              />
  
              <q-input :rules="[val => !!val || 'required']"
                v-model="movie.audioPromocionalUrl"
                label="Promotional Audio URL"
                outlined rounded
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
  { label: 'G - General Audiences', value: 'g', description: 'All ages admitted. Nothing that would offend parents if viewed by children.' },
  { label: 'PG - Parental Guidance Suggested', value: 'pg', description: 'Some material may not be suitable for children. Parents urged to give "parental guidance." May contain some material parents might not like for their young children.' },
  { label: 'PG-13 - Parents Strongly Cautioned', value: 'pg13', description: 'Some material may be inappropriate for children under 13. Parents are urged to be cautious. Some material may be inappropriate for pre-teenagers.' },
  { label: 'R - Restricted', value: 'r', description: 'Restricted. People under 17 require accompanying parent or adult guardian. May contain strong language, violence, sexual content, or drug use.' }
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
  
  