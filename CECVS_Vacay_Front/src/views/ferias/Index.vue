<template>
    <h1>Férias</h1>
    <div class="py-3" />
    <v-btn variant="elevated" color="primary" :to="{ name: FERIAS_CREATE }">Nova</v-btn>
    <TabelaFerias :ferias="ferias" />
</template>


<script setup lang="ts">

import TabelaFerias from '@/components/TabelaFerias'
import { FERIAS_CREATE } from '@/router/router-types';

</script>

<script lang="ts">

import { computed, onMounted, ref, Ref } from 'vue';
import { getFeriasList } from '@/apis/ferias'

import DataTable from 'datatables.net-vue3'
import DataTableLib from 'datatables.net-bs5'
import 'datatables.net-select-bs5';

DataTable.use(DataTableLib);

export default {
    components: { DataTable },
    data() {
        return {
            ferias: [],
        }
    },
    mounted() {
        this.carregaFeriasList();
    },
    methods: {
        async carregaFeriasList() {
            getFeriasList().then((data) => {

                console.log(data);

                this.ferias = data;
            });
        },
    },
};

</script>