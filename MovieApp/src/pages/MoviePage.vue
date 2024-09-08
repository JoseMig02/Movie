<template>
  <q-page>
    <q-btn @click="openModal" label="Add Movie" color="primary" />
    
    <q-dialog v-model="isModalOpen" persistent>
      <q-card style="min-width: 600px">
        <q-card-section>
          <q-form @submit.prevent="submitForm" class="q-pa-md">
            <div class="q-mb-md">
              <q-input
                v-model="movie.title"
                label="Title"
                required
              />
            </div>

            <div class="q-mb-md">
              <q-input
                v-model="movie.description"
                label="Description"
                type="textarea"
                rows="4"
              />
            </div>

            <div class="q-mb-md">
              <q-select
                v-model="movie.genre"
                :options="genres"
                label="Genre"
                required
              />
            </div>

            <div class="q-mb-md">
              <q-select
                v-model="movie.rating"
                :options="ratings"
                label="Rating"
                required
              />
            </div>

            <div class="q-mb-md">
              <q-checkbox
                v-model="movie.isFavorite"
                label="Is Favorite"
              />
            </div>

            <div class="q-mb-md">
              <q-radio
                v-model="movie.ageRating"
                :options="ageRatings"
                label="Age Rating"
              />
            </div>

            <div class="q-mb-md">
              <q-img
                v-if="movie.imageUrl"
                :src="movie.imageUrl"
                class="q-mb-md"
                style="max-width: 200px;"
              />
              <q-input
                v-model="movie.imageUrl"
                label="Image URL"
                type="text"
              />
            </div>

            <div class="q-mb-md">
              <q-input
                v-model="movie.videoUrl"
                label="Video URL"
                type="text"
              />
            </div>

            <div class="q-mb-md">
              <q-input
                v-model="movie.audioUrl"
                label="Audio URL"
                type="text"
              />
            </div>

            <div class="q-mb-md">
              <q-input
                v-model="movie.releaseDate"
                type="date"
                label="Release Date"
              />
            </div>

            <div class="q-mb-md">
              <q-btn
                label="Submit"
                type="submit"
                color="primary"
                class="q-mr-sm"
              />
              <q-btn
                label="Cancel"
                color="secondary"
                @click="closeModal"
              />
            </div>
          </q-form>
        </q-card-section>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { ref } from 'vue';

const isModalOpen = ref(false);

const movie = ref({
  title: '',
  description: '',
  genre: null,
  rating: null,
  isFavorite: false,
  ageRating: '',
  imageUrl: '',
  videoUrl: '',
  audioUrl: '',
  releaseDate: ''
});

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

const ageRatings = ref([
  { label: 'All Ages', value: 'all' },
  { label: '12+', value: '12+' },
  { label: '18+', value: '18+' }
]);

const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

const submitForm = () => {
  console.log('Form submitted:', movie.value);
  // Aquí puedes hacer una llamada a la API para guardar la película
  closeModal();
};
</script>

<style scoped>
.q-page {
  max-width: 600px;
  margin: 0 auto;
}
</style>
