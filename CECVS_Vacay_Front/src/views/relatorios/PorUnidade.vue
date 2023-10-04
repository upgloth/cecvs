<template>
    <h1>Férias na Unidade</h1>
    <div class="py-3" />

    <TabelaFerias :ferias="ferias" />
</template>


<script setup lang="ts">

import TabelaFerias from '@/components/TabelaFerias'
import { onMounted, ref } from 'vue';
import { getFeriasList } from '@/apis/ferias'


const ferias = ref([]);

onMounted(() => {

    getFeriasList().then((data) => {

        console.log(data);

        ferias.value = data;
    });
});

</script>