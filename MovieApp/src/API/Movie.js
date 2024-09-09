import axios from 'axios';

const API_URL = 'https://localhost:7265/api/Movie'; // Asegúrate de usar la URL correcta para tu API

// Obtener todas las películas
export const getMovies = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching movies:', error);
    throw error;
  }
};

// Obtener una película por ID
export const getMovie = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching movie with ID ${id}:`, error);
    throw error;
  }
};

// Crear una nueva película
export const createMovie = async (movie) => {
  try {
    const response = await axios.post(API_URL, movie);
    return response.data;
  } catch (error) {
    console.error('Error creating movie:', error);
    throw error;
  }
};

// Actualizar una película existente
export const updateMovie = async (id, movie) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, movie);
    return response.data;
  } catch (error) {
    console.error(`Error updating movie with ID ${id}:`, error);
    throw error;
  }
};

// Eliminar una película
export const deleteMovie = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error(`Error deleting movie with ID ${id}:`, error);
    throw error;
  }
};
