<template>
  <q-btn id="AddMovie" @click="openModal" label="Add Movie" color="primary" />

  <!-- Modal Trigger Button -->

  <!-- Modal with Form -->
  <q-dialog v-model="isModalOpen">
    <q-card style="min-width: 600px; padding: 20px">
      <q-card-section>
        <q-form @submit.prevent="submitForm">
          <!-- Form Controls -->
          <q-input
            name="title"
            :rules="[(val) => !!val || 'required']"
            v-model="movie.titulo"
            label="Title"
            outlined
            rounded
          />
          <div class="q-my-md">
            <label for="genres">Género</label>
            <select
              style="
                width: 100%;
                height: 55px;
                border-radius: 50px;
                border: 1px solid #ccc;
                outline: 0;
              "
              id="genres"
              v-model="movie.genero"
              :required="true"
            >
              <option value="" disabled selected>Seleccione un género</option>
              <option
                v-for="genre in genres"
                :key="genre.value"
                :value="genre.value"
              >
                {{ genre.label }}
              </option>
            </select>
          </div>

          <div class="q-my-md">
            <label for="clasification">Clasificación</label>
            <select
              style="
                width: 100%;
                height: 55px;
                border-radius: 50px;
                border: 1px solid #ccc;
                outline: 0;
              "
              id="clasification"
              v-model="movie.clasificacion"
              required
            >
              <option value="" disabled selected>
                Seleccione una clasificación
              </option>
              <option
                v-for="rating in ratings"
                :key="rating.value"
                :value="rating.value"
              >
                {{ rating.label }}
              </option>
            </select>
          </div>
          <div class="q-mb-md">
            <label for="inTheater" class="checkbox-label">En Cartelera</label>
            <input  
              type="checkbox"
              id="inTheater"
              v-model="movie.enCartelera"
              class="styled-checkbox"
            />
            <span class="checkmark"></span>
          </div>

          <q-input
            :rules="[(val) => !!val || 'required']"
            name="dateRelease"
            v-model="movie.fechaEstreno"
            type="date"
            label="Release Date"
            outlined
            rounded
          />

          <q-input
            :rules="[(val) => !!val || 'required']"
            v-model="movie.imagenUrl"
            name="Image"
            label="Image URL"
            outlined
            rounded
          />

          <q-input
            :rules="[(val) => !!val || 'required']"
            v-model="movie.sinopsis"
            name="synopsis"
            type="textarea"
            label="Synopsis"
            outlined
            rounded
          />

          <q-input
            :rules="[(val) => !!val || 'required']"
            v-model="movie.trailerUrl"
            label="Trailer URL"
            name="trailerUrl"
            outlined
            rounded
          />

          <q-input
            :rules="[(val) => !!val || 'required']"
            v-model="movie.audioPromocionalUrl"
            name="audioPromotional"
            label="Promotional Audio URL"
            outlined
            rounded
          />

          <q-btn type="submit" label="Submit" color="primary" />
          <q-btn @click="closeModal" label="Cancel" color="secondary" flat />
        </q-form>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>
  
  <script setup>
import { ref } from "vue";
import { createMovie } from "../API/Movie";

const isModalOpen = ref(false);
const emit = defineEmits(["add-movie"]);

const movie = ref({
  titulo: "",
  genero: null,
  clasificacion: null,
  enCartelera: false,
  fechaEstreno: "",
  imagenUrl: "",
  sinopsis: "",
  trailerUrl: "",
  audioPromocionalUrl: "",
});

const genres = ref([
  { label: "Action", value: "action" },
  { label: "Comedy", value: "comedy" },
  { label: "Drama", value: "drama" },
]);
const ratings = ref([
  {
    label: "G - General Audiences",
    value: "g",
    description:
      "All ages admitted. Nothing that would offend parents if viewed by children.",
  },
  {
    label: "PG - Parental Guidance Suggested",
    value: "pg",
    description:
      'Some material may not be suitable for children. Parents urged to give "parental guidance." May contain some material parents might not like for their young children.',
  },
  {
    label: "PG-13 - Parents Strongly Cautioned",
    value: "pg13",
    description:
      "Some material may be inappropriate for children under 13. Parents are urged to be cautious. Some material may be inappropriate for pre-teenagers.",
  },
  {
    label: "R - Restricted",
    value: "r",
    description:
      "Restricted. People under 17 require accompanying parent or adult guardian. May contain strong language, violence, sexual content, or drug use.",
  },
]);

const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

const submitForm = async () => {
    
  try {
    await createMovie(movie.value);

    closeModal();
    emit("add-movie");
  } catch (error) {
    console.error("Error submitting form:", error);
  }
};
</script>
  

