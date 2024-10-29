<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
        />

        <q-toolbar-title>
          Quasar App
        </q-toolbar-title>

        <q-toolbar-title>
        <div v-if="weatherData">
          <div>
            {{ weatherData.name }}: {{ weatherData.main.temp }}°C, {{ weatherData.weather[0].description }}
          </div>
        </div>
        
        <div v-else>
          Cargando clima...
        </div>

        </q-toolbar-title>
    
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { getWeather } from 'src/API/Movie';

const weatherData = ref(null)


const fetchWeather = async () => {
      const {data}= await getWeather();
      console.log(data)
      weatherData.value = data;
    };

    onMounted(() => {
      fetchWeather();
    });



</script>
