<template>
    <div>
      <q-btn
        flat
        round
        dense
        color="negative"
        icon="delete"
        @click="confirmDelete"
      />
    
      <q-dialog v-model="showConfirm">
        <q-card>
          <q-card-section class="row items-center">
            <q-icon name="warning" color="negative" />
            <span class="q-ml-sm">¿Estás seguro de que deseas eliminar esta película?</span>
          </q-card-section>
    
          <q-card-section>
            <q-btn flat label="Cancelar" color="primary" @click="showConfirm = false" />
            <q-btn flat label="Eliminar" color="negative" @click="deleteMovie" />
          </q-card-section>
        </q-card>
      </q-dialog>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  
  const props = defineProps({
    id: {
      type: Number,
      required: true,
    },
  });
  
  const emit = defineEmits(['delete-movie']);
  const showConfirm = ref(false);
  
  const confirmDelete = () => {
    showConfirm.value = true;
  };
  
  const deleteMovie = () => {
    emit('delete-movie', props.id);
    showConfirm.value = false;
  };
  </script>
  
  <style scoped>
  .q-btn {
    min-width: 36px;
    height: 36px;
  }
  </style>
  