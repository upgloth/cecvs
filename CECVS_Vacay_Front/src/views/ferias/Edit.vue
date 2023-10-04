<template>
     <FormFerias :id-ferias="id"/>   
    
</template>


<script setup lang="ts">

import FormFerias from '@/components/FormFerias.vue';
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";


const  id = ref();

onMounted   (()=>{
    const route = useRoute();
    id.value = route.params.id;

    console.log(id.value);
});
</script>