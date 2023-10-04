<template>
    <h1>Férias</h1>
    <div class="py-3" />
    <v-btn variant="elevated" color="primary" :to="{ name: FERIAS_CREATE }">Nova</v-btn>

    <TabelaFerias :ferias="ferias" />
</template>


<script setup lang="ts">
import { FERIAS_CREATE } from '@/router/router-types'
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