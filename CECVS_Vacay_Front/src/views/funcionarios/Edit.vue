<template>
    <FormFuncionario :id-funcionario="id"/>   
    
</template>


<script setup lang="ts">

import FormFuncionario from '@/components/FormFuncionario.vue';
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";


const  id = ref();

onMounted   (()=>{
    const route = useRoute();
    id.value = route.params.id;
});
</script>