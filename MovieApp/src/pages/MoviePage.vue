<template>
  <q-page class="q-pa-md bg-grey-1">
    <q-card flat bordered class="q-pa-md">
      <q-card-section>
        <div class="text-h6">Añadir Película</div>
        <AddMovie @add-movie="handleAdd" />
      </q-card-section>

      <q-card-section>
        <div class="text-h6">Lista de Películas</div>
      </q-card-section>

      <q-table :rows="movies" :columns="columns" row-key="id" flat bordered separator="cell" dense>
        <template v-slot:body-cell-actions="props">
          <q-td align="center" :props="props">
            <EditMovie :movie="props.row" />
            <DeleteMovie :id="props.row.id" @delete-movie="handleDeleteMovie" />
          </q-td>
        </template>
        <template v-slot:body-cell-imagenUrl="props">
          <q-td align="center" :props="props">
            <q-img :src="props.row.imagenUrl" alt="Imagen película" style="width: 70px; height: 75px" />
          </q-td>
        </template>
        <template v-slot:body-cell-trailer="props">
          <q-td align="center" :props="props">
            <q-btn flat icon="play_circle" color="primary" @click="openTrailer(props.row.trailerUrl)" />
          </q-td>
        </template>
      </q-table>

      <!-- Modal para reproducir el tráiler -->
      <q-dialog v-model="isTrailerOpen" persistent>
        <q-card>
          <q-card-section>
            <iframe
              :src="currentTrailerUrl"
              width="560"
              height="315"
              frameborder="0"
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
              allowfullscreen>
            </iframe>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn flat label="Cerrar" color="primary" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import AddMovie from '../components/AddMovie.vue';
import DeleteMovie from 'src/components/DeleteMovie.vue';
import EditMovie from '../components/EditMovie.vue';
import { getMovies, deleteMovie } from '../API/Movie'; // Ajusta la ruta según tu proyecto

const movies = ref([]);
const isTrailerOpen = ref(false);
const currentTrailerUrl = ref('');

const columns = [
  { name: 'titulo', label: 'Título', align: 'left', field: 'titulo', sortable: true },
  { name: 'genero', label: 'Género', align: 'left', field: 'genero', sortable: true },
  { name: 'clasificacion', label: 'Clasificación', align: 'left', field: 'clasificacion', sortable: true },
  { name: 'fechaEstreno', label: 'Fecha de Estreno', align: 'left', field: 'fechaEstreno', sortable: true },
  { name: 'enCartelera', label: 'En Cartelera', align: 'center', field: 'enCartelera' },
  { name: 'imagenUrl', label: 'Imagen', align: 'center', field: 'imagenUrl' }, // Nueva columna para imagen
  { name: 'trailer', label: 'Tráiler', align: 'center' }, // Nueva columna para tráiler
  { name: 'actions', label: 'Acciones', align: 'center' },
];

// Función para abrir el tráiler
const openTrailer = (url) => {
  currentTrailerUrl.value = url.replace('watch?v=', 'embed/');
  isTrailerOpen.value = true;
};

// Cargar las películas desde la API
const fetchMovies = async () => {
  try {
    movies.value = await getMovies();
  } catch (error) {
    console.error('Error fetching movies:', error);
  }
};

// Manejar la eliminación de una película
const handleDeleteMovie = async (id) => {
  try {
    await deleteMovie(id);
    await fetchMovies(); // Recargar la lista de películas
  } catch (error) {
    console.error('Error deleting movie:', error);
  }
};

const handleAdd = async () => {
  await fetchMovies();
};

onMounted(() => {
  fetchMovies();
});
</script>

<style scoped>
.q-page {
  max-width: 1200px;
  margin: 0 auto;
}

.q-btn {
  min-width: 36px;
  height: 36px;
}

.q-card {
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
}

.q-table {
  border-radius: 8px;
  overflow: hidden;
}

.q-icon {
  font-size: 24px;
}

.q-table th {
  background-color: #f0f0f0;
  text-transform: uppercase;
  font-weight: bold;
}
</style>
