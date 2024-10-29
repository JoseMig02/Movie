import axios from 'axios';

const API_URL = 'https://localhost:7265/api/Movie'; 

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

export const getMovie = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching movie with ID ${id}:`, error);
    throw error;
  }
};

export const createMovie = async (movie) => {
  try {
    const response = await axios.post(API_URL, movie);
    return response.data;
  } catch (error) {
    console.error('Error creating movie:', error);
    throw error;
  }
};

export const updateMovie = async (id, movie) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, movie);
    return response.data;
  } catch (error) {
    console.error(`Error updating movie with ID ${id}:`, error);
    throw error;
  }
};

export const deleteMovie = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error(`Error deleting movie with ID ${id}:`, error);
    throw error;
  }
};

export const getWeather = async () => {
  try {
    const response = await axios.get('https://api.openweathermap.org/data/2.5/weather?lat=18.4734958&lon=-69.9148034&appid=b12b6953a3bd3296890ab6cfea148d16')
    return response;
    } catch (error) {
      console.error('Error fetching weather:', error);
  }

}
