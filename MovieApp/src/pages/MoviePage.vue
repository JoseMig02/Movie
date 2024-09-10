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
                
                <EditMovie @click="editMovie(props.row)" />
                <!-- Se pasa el ID de la película a DeleteMovie -->
                <DeleteMovie :id="props.row.id" @delete-movie="handleDeleteMovie" />
              </q-td>
            </template>
          </q-table>
        </q-card>
      </q-page>
    </template>

    <script setup>
    import { ref, onMounted, watch } from 'vue';
    import AddMovie from '../components/AddMovie.vue';
    import DeleteMovie from 'src/components/DeleteMovie.vue';
    import EditMovie from '../components/EditMovie.vue'
    import { getMovies, deleteMovie } from '../API/Movie'; // Ajusta la ruta según tu proyecto

    const movies = ref([]);
    const columns = [
      { name: 'titulo', label: 'Título', align: 'left', field: 'titulo', sortable: true },
      { name: 'genero', label: 'Género', align: 'left', field: 'genero', sortable: true },
      { name: 'clasificacion', label: 'Clasificación', align: 'left', field: 'clasificacion', sortable: true },
      { name: 'fechaEstreno', label: 'Fecha de Estreno', align: 'left', field: 'fechaEstreno', sortable: true },
      { name: 'enCartelera', label: 'En Cartelera', align: 'center', field: 'enCartelera' },
      { name: 'actions', label: 'Acciones', align: 'center' },
    ];

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
    }

    // Editar una película
    const editMovie = (movie) => {
      console.log('Edit movie:', movie);
    };

    onMounted(() => {
      fetchMovies();
    });

    watch(() => movies.value, () => {
      fetchMovies()
    }, { immediate: true });
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
