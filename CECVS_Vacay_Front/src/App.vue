<template>
  <router-view />
</template>

<script lang="ts">
import { SET_DEPARTAMENTOS } from '@/store/mutation-types';
import { getDepartamentos } from '@/apis/departamentos'

export default {
  created() {    
    this.carregaDepartamentos();
  },
  methods: {
    async carregaDepartamentos(){
       try {
         const response = await getDepartamentos();
         this.$store.dispatch(SET_DEPARTAMENTOS, response);

       } catch (error) {
         console.error('Erro ao carregar departamentos', error);
       }
    },          
  }
};
</script>
