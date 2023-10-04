import axios, { AxiosInstance } from 'axios'
import router from '@/router'

const apiV1 = axios.create({
    baseURL: '/api'
});

apiV1.interceptors.response.use(
    function (response) {
        return response;
    },
    function (error) {
        console.log(error)
        
        // if (error.response?.status != 200) {
        //     //
        // }
    });

export { apiV1 };
