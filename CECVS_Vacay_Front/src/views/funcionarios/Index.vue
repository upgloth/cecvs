<template>
    <h1>Funcionário</h1>
    <div class="py-3" />
    <v-btn variant="elevated" color="primary" :to="{ name: FUNCIONARIO_CREATE }">Novo</v-btn>

    <div class="row">
        <div>
            <div class="table-responsive">
                <DataTable :data="funcionarios" :columns="colunas" :options="{
                    autoWidth: false, dom: 'Bfrtip', select: false,
                    language: {
                        search: 'Pesquisar', zeroRecords: 'Sem registros',
                        info: 'Mostrando registros _START_ até _END_ de _TOTAL_',
                        paginate: { first: 'Primeiro', previous: 'Anterior', next: 'Próximo', last: 'Último' }
                    }
                }" class="table table-striped table-bordered display">
                    <thead>
                        <tr>
                            <th>
                                Matrícula
                            </th>
                            <th>
                                Nome
                            </th>
                            <th>
                                Admissão
                            </th>
                            <!-- <th>
                                Departamento
                            </th> -->
                            <th>Ações</th>
                        </tr>
                    </thead>
                </DataTable>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { FUNCIONARIO_CREATE } from '@/router/router-types'
</script>

<script lang="ts">

import { computed, onMounted, ref, Ref } from 'vue';
import { getFuncionarios } from '@/apis/funcionarios'

import DataTable from 'datatables.net-vue3'
import DataTableLib from 'datatables.net-bs5'
import 'datatables.net-select-bs5';

DataTable.use(DataTableLib);

export default {
    components: { DataTable },
    data() {
        return {
            funcionarios: [],
            colunas: [
                { data: 'coMatricula', width: '10%' },
                { data: 'noFuncionario' },
                {
                    data: null,
                    width: '15%',
                    render: function (data, type, row, meta) {
                        return `${data.dtAdmissao.substring(0,10)}`;
                    },
                },
                //{ data: 'idDepartamento', width: '25%' },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return `<a href="/funcionarios/${data.idFuncionario}" class="">Visualizar</a> `;
                        //  <a href="/funcionarios/${data.idFuncionario}/ferias" class="">Férias</a>`;
                    },
                    width: '10%'
                },
            ],
        }
    },
    mounted() {
        this.carregaFuncionarios();
    },
    methods: {
        async carregaFuncionarios() {
            getFuncionarios()
                .then((response) => {
                    this.funcionarios = response;
                });
        },
    },
};

</script>

<style>
@import 'datatables.net-bs5';
</style>