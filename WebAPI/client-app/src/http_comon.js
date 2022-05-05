import axios from "axios";

export const baseURL = "http://localhost:5147/"


const myAxios = axios.create({
    baseURL: baseURL,
    headers: {
        "Content-type": "application/json",
    }
});
myAxios.interceptors.request.use(
    (config) => {
        let token = localStorage.getItem('token')

        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`
        }
        return config
    },
    (error) => {
        return Promise.reject(error)
    }
)

export default myAxios;

