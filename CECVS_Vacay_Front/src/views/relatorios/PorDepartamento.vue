<template>
    <h1>Férias no Departamento</h1>
    <div class="py-3" />
    <v-select v-model="idDepartamento" :items="$store.state.departamentos" item-title="noDepartamento"
        item-value="idDepartamento" label="Departamento">
    </v-select>

    <TabelaFerias :ferias="ferias" />
</template>


<script setup lang="ts">

import TabelaFerias from '@/components/TabelaFerias';
import { onMounted, ref, watch } from 'vue';
import { getFeriasByIdDepartamento } from '@/apis/ferias'

const ferias = ref([]);
const idDepartamento = ref();

// onMounted(() => {


// });

watch(idDepartamento, (newidDepartamento) => {
    if (newidDepartamento > 0) {
        getFeriasByIdDepartamento(newidDepartamento)
            .then((response) => {
                ferias.value = response;
            });
    }
});





</script>

<!-- <script lang="ts">

import { getFeriasByIdDepartamento } from '@/apis/ferias'
import { labeledStatement } from '@babel/types';

export default {
    data() {
        return {
            //ferias: [],
            idDepartamento: null,
        }
    },
    mounted() {
    },
    methods: {
        async carregaRelatorio() {
            getFeriasByIdDepartamento(this.idDepartamento)
                .then((response) => {
                    $ferias.value = response;
                });
        },
    },
    watch: {
        idDepartamento(newValue) {
            console.log(newValue);
            this.carregaRelatorio();
        }
    },
};

</script> -->