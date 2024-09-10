<template>
    <div>
      <!-- Edit Button -->
      <q-btn @click="openModal" flat round dense color="primary" icon="edit" />
  
      <!-- Modal with Form -->
      <q-dialog v-model="isModalOpen">
        <q-card style="min-width: 600px; padding: 20px;">
          <q-card-section>
            <q-form @submit.prevent="submitForm">
              <!-- Form Controls -->
              <q-input v-model="localMovie.titulo" label="Title"
                :rules="[val => !!val || 'Title is required']" outlined rounded />
  
              <q-select v-model="localMovie.genero" :options="genres" label="Genre" option-label="label"
                option-value="value" :rules="[val => !!val || 'Genre is required']" outlined rounded />
  
              <q-select v-model="localMovie.clasificacion" :options="ratings" label="Rating"
                option-label="label" option-value="value" :rules="[val => !!val || 'Rating is required']"
                outlined rounded />
  
              <q-input v-model="actorInput" label="Actors" @keyup.enter="addActor" outlined rounded />
              <q-chip v-for="(actor, index) in localMovie.actores" :key="index" removable
                @remove="removeActor(index)">
                {{ actor }}
              </q-chip>
  
              <q-toggle v-model="localMovie.enCartelera" label="In Theaters" />
  
              <q-input v-model="localMovie.fechaEstreno" type="date" label="Release Date"
                :rules="[val => !!val || 'Release date is required']" outlined rounded />
  
              <q-input v-model="localMovie.imagenUrl" label="Image URL"
                :rules="[val => !!val || 'Image URL is required']" outlined rounded />
  
              <q-input v-model="localMovie.sinopsis" type="textarea" label="Synopsis"
                :rules="[val => !!val || 'Synopsis is required']" outlined rounded />
  
              <q-input v-model="localMovie.trailerUrl" label="Trailer URL"
                :rules="[val => !!val || 'Trailer URL is required']" outlined rounded />
  
              <q-input v-model="localMovie.audioPromocionalUrl" label="Promotional Audio URL" outlined
                rounded />
  
              <q-btn type="submit" label="Save Changes" color="primary" />
              <q-btn @click="closeModal" label="Cancel" color="secondary" flat />
            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>
    </div>
  </template>
  
  <script setup>
  import { ref, watch } from 'vue';
  import { updateMovie } from '../API/Movie'; // Ajusta la ruta según tu proyecto
  
  const props = defineProps({
    movie: Object,
  });
  
  const emit = defineEmits(['update-movie']);
  
  const isModalOpen = ref(false);
  
  const localMovie = ref({
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
    if (props.movie) {
        console.log(props.movie)
      // Cargar los datos de la película en localMovie
      localMovie.value = { ...props.movie, genero: genres.value.find(g => g.value === props.movie.genero), clasificacion: ratings.value.find(r => r.value === props.movie.clasificacion) };
    }
    isModalOpen.value = true;
  };
  
  const closeModal = () => {
    isModalOpen.value = false;
  };
  
  const submitForm = async () => {
    try {
      await updateMovie(props.movie.id, {
        ...localMovie.value,
        genero: localMovie.value.genero?.value, // Send only value
        clasificacion: localMovie.value.clasificacion?.value // Send only value
      });
  
      closeModal();
      emit('update-movie');
    } catch (error) {
      console.error('Error submitting form:', error);
    }
  };
  
  const addActor = () => {
    if (actorInput.value.trim() !== '') {
      localMovie.value.actores.push(actorInput.value.trim());
      actorInput.value = '';
    }
  };
  
  const removeActor = (index) => {
    localMovie.value.actores.splice(index, 1);
  };
  
  // Watch for movie prop changes
  watch(() => props.movie, (newValue) => {
    if (newValue) {
      localMovie.value = { ...newValue, genero: genres.value.find(g => g.value === newValue.genero), clasificacion: ratings.value.find(r => r.value === newValue.clasificacion) };
    }
  }, { immediate: true });
  </script>
  
  <style scoped>
  /* Añadir estilos según sea necesario */
  </style>
  