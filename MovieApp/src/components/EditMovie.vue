<template>
    <div>
      <q-btn @click="openModal" flat round dense color="primary" icon="edit" />
      <q-dialog v-model="isModalOpen">
        <q-card style="min-width: 600px; padding: 20px;">
          <q-card-section>
            <q-form @submit.prevent="submitForm">
              <q-input v-model="localMovie.titulo" label="Title"
                :rules="[val => !!val || 'Title is required']" outlined rounded />
  
              <q-select v-model="localMovie.genero" :options="genres" label="Genre" option-label="label"
                option-value="value" :rules="[val => !!val || 'Genre is required']" outlined rounded />
  
              <q-select v-model="localMovie.clasificacion" :options="ratings" label="Rating"
                option-label="label" option-value="value" :rules="[val => !!val || 'Rating is required']"
                outlined rounded />
  
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
  
              <q-btn class="q-mt-md" type="submit" label="Save Changes" color="primary" />
              <q-btn class="q-mt-md" @click="closeModal" label="Cancel" color="secondary" flat />
            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>
    </div>
  </template>
  
  <script setup>
  import { ref, watch } from 'vue';
  import { updateMovie } from '../API/Movie'; 
  
  const props = defineProps({
    movie: Object,
  });
  
  const emit = defineEmits(['update-movie']);
  
  const isModalOpen = ref(false);
  
  const localMovie = ref({
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
    if (props.movie) {
        console.log(props.movie)
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
        genero: localMovie.value.genero?.value, 
        clasificacion: localMovie.value.clasificacion?.value 
      });
  
      closeModal();
      emit('update-movie');
    } catch (error) {
      console.error('Error submitting form:', error);
    }
  };
  

  watch(() => props.movie, (newValue) => {
    if (newValue) {
      localMovie.value = { ...newValue, genero: genres.value.find(g => g.value === newValue.genero), clasificacion: ratings.value.find(r => r.value === newValue.clasificacion) };
    }
  }, { immediate: true });
  </script>
  
 