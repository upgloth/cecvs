<template>
    <div class="row">
        <div >
            <div class="table-responsive">
                <DataTable :data="ferias" :columns="colunas" :options="{
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
                                Data Ínicio
                            </th>
                            <th>
                                Dias
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


<script lang="ts">
import { computed, onMounted, Ref } from 'vue';
import store from '@/store';

import DataTable from 'datatables.net-vue3'
import DataTableLib from 'datatables.net-bs5'
import 'datatables.net-select-bs5';

DataTable.use(DataTableLib);

export default {
    components: { DataTable },
    props: {
        ferias: [],
    },
    data() {
        return {
            colunas: [
                { data: 'coMatricula', width: '15%' },
                { data: 'noFuncionario' },               
                {
                    data: null,
                    width: '15%',
                    render: function (data, type, row, meta) {
                        return `${data.dtInicio.substring(0,10)}`;
                    },
                },
                { data: 'qtDias' , width: '15%'},
                //{ data: 'noDepartamento' },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return `<a href="/ferias/${data.idFerias}" class="">Visualizar</a>   `;
                    },
                    width: '10%'
                },

            ],
        }
    },
};

</script>

<style>
@import 'datatables.net-bs5';
</style>