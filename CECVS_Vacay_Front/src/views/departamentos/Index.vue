<template>
    <h1>Departamentos</h1>
    <div class="py-3" />
    <v-btn variant="elevated" color="primary" :to="{ name: DEPARTAMENTO_CREATE }">Novo</v-btn>

    <div class="row">
        <div>
            <div class="table-responsive">
                <DataTable :data="departamentos" :columns="colunas" :options="{
                    autoWidth: false, dom: 'Bfrtip', select: false,
                    language: {
                        search: 'Pesquisar', zeroRecords: 'Sem registros',
                        info: 'Mostrando registros _START_ até _END_ de _TOTAL_',
                        paginate: { first: 'Primeiro', previous: 'Anterior', next: 'Próximo', last: 'Último' }
                    }
                }" class="table table-striped table-bordered display">
                    <thead>
                        <tr>
                            <!-- <th>
                                #
                            </th> -->
                            <th>
                                Nome
                            </th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                </DataTable>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { DEPARTAMENTO_CREATE } from '@/router/router-types'
</script>

<script lang="ts">

import { computed, onMounted, Ref } from 'vue';
import store from '@/store';
import { IDepartamentoDTO } from '@/models/departamento';
import { getDepartamentos } from '@/apis/departamentos'

import DataTable from 'datatables.net-vue3'
import DataTableLib from 'datatables.net-bs5'
import 'datatables.net-select-bs5';

DataTable.use(DataTableLib);

export default {
    components: { DataTable },
    data() {
        return {
            departamentos: [],
            colunas: [
                //{ data: 'idDepartamento', width: '10%' },
                { data: 'noDepartamento' },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return `<a href="/departamentos/${data.idDepartamento}" class="">Visualizar</a>   `;
                    },
                    width: '10%'
                },

            ],
        }
    },
    mounted() {
        this.carregaDepartamentos();
    },
    methods: {
        async carregaDepartamentos() {
            getDepartamentos()
                .then((response) => {
                    this.departamentos = response;
                });
        },
    },
};

</script>

<style>
@import 'datatables.net-bs5';
</style>