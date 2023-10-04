<template>
    <h1>Férias</h1>

    <v-alert v-if="showAlertaDeletar" type="warning" title="Confirmação Necessária" border="start"
        text="Deletar registro de férias, continuar?">
        <div class="py-1" />
        <v-row class="justify-content-end">
            <v-col cols="auto">
                <v-btn variant="elevated" color="primary" @click="showAlertaDeletar = false" :disabled="loading">Não</v-btn>
            </v-col>
            <v-col cols="auto">
                <v-btn variant="elevated" @click="deletar()" :disabled="(!editando || loading)">Sim</v-btn>
            </v-col>
        </v-row>
    </v-alert>

    <!-- <div class="py-3" />
    <div v-for="alerta in alertas">
        <v-alert :type="alerta.type" :title="alerta.title" border="start" closable :text="alerta.text" />
    </div> -->
    <v-responsive class="align-center text-center  justify-center">
        <div class="py-3" />
        <v-form v-model="valid" @submit.prevent>

            <v-select v-model="idFuncionario" :items="funcionarios" item-title="noFuncionario" item-value="idFuncionario"
                :rules="idFuncionarioRules" label="Funcionário" v-bind:readonly="editando">
            </v-select>

            <v-text-field v-model="dtInicio" type="date" :rules="dtInicioRules" label="Matrícula" />

            <v-text-field v-model="qtDias" type="number" :rules="qtDiasRules" label="Quantidade Dias" />

            <div class="py-3" />

            <v-row class="justify-content-end">
                <v-col cols="auto">
                    <v-btn variant="elevated" color="primary" @click="$router.go(-1)" :disabled="loading">Voltar</v-btn>
                </v-col>
                <v-col cols="auto">
                    <v-btn v-if="!editando" variant="elevated" color="gray" @click="salvar()"
                        :disabled="loading">Salvar</v-btn>
                    <v-btn v-if="editando" variant="elevated" color="gray" @click="atualizar()"
                        :disabled="loading">Atualizar</v-btn>
                </v-col>
                <v-col cols="auto">
                    <v-btn variant="elevated" color="red-lighten-2" @click="confirmarDeletar()"
                        :disabled="(!editando || loading)">Deletar</v-btn>
                </v-col>
                <v-col cols="auto">
                    <v-btn variant="elevated" color="blue-grey-lighten-1" @click="$router.go(-1)"
                        :disabled="loading">Cancelar</v-btn>
                </v-col>

            </v-row>
            <div class="py-3" />
        </v-form>
    </v-responsive>

    <v-alert v-if="showErros" type="error" title="Erro" border="start" :text="errorText">
        <div class="py-1" />
        <v-row class="justify-content-end">
            <v-col cols="auto">
                <v-btn variant="elevated" color="primary" @click="showErros = false" :disabled="loading">Ok</v-btn>
            </v-col>
        </v-row>
    </v-alert>
</template>


<script lang="ts">
import { ref } from "vue";
import { deleteFuncionario, getFuncionario, getFuncionarios, postFuncionario, putFuncionario } from '@/apis/funcionarios';
import { IFuncionarioDTO } from '@/models/funcionario';
import { deleteFerias, getFerias, postFerias, putFerias } from "@/apis/ferias";
import { IFeriasDTO, IFeriasUpdateDTO } from "@/models/ferias";

export default {
    props: {
        idFerias: null,
    },
    data() {
        return {
            valid: false,
            funcionarios: [],
            idFuncionario: null,
            dtInicio: '',
            qtDias: null,
            editando: false,
            loading: true,
            showAlertaDeletar: false,
            showErros: false,
            errorText: 'Erro ao executar operação (Nesta versão o erro pode ser verificado no console).',
            idFuncionarioRules: [
                (value: number) => {
                    if (value && value > 0) return true;

                    return 'Funcionário não selecionado.';
                }
            ],
            dtInicioRules: [
                (value: string) => {
                    if (value && value.length >= 10) return true;

                    return 'Data Ínicio férias não selecionado.';
                },
                (value: string) => {
                    const parsedDate = new Date(value);
                    parsedDate.setDate(parsedDate.getDate() + 5);

                    console.log(parsedDate)
                    console.log(new Date())

                    if (parsedDate < new Date()) return true;

                    return 'A data de início deve ser superior à data atual + 5 dias.';
                }
            ],
            qtDiasRules: [
                (value: number) => {
                    if (value && value >= 5 && value <= 30) return true;

                    return 'Quantidade de dias deve ser entre 5 e 30.';
                }
            ],
            alertas: [
                // { type: "success", title: "Success title", text: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore voluptatibus..." },
                // { type: "warning", title: "Warning title", text: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore voluptatibus..." },
                // { type: "error", title: "Error title", text: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore voluptatibus..." },
                // { type: "info", title: "Info title", text: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore voluptatibus..." }
            ]
        }
    },
    methods: {
        async carregaFerias() {
            // Carrega departamento
            const ferias = await getFerias(this.idFerias);

            this.idFuncionario = ferias.idFuncionario;
            this.dtInicio = ferias.dtInicio.substring(0, 10);
            this.qtDias = ferias.qtDias;
        },
        async carregaFuncionarios() {
            // Carrega funcionario 
            const funcionarios = await getFuncionarios();

            this.funcionarios = funcionarios;
        },
        async salvar() {
            if (this.valid) {
                try {
                    const payload: IFeriasDTO = {
                        idFerias: 0,
                        idFuncionario: this.idFuncionario,
                        dtInicio: this.dtInicio,
                        qtDias: this.qtDias
                    }

                    const response = await postFerias(payload);

                    //console.log(response);

                    if (response) {
                        // Sucesso, volta para a lista de items
                        this.$router.go(-1);
                    }
                    else {
                        // Erro, mostra erro
                        this.showErros = true;
                    }

                }
                catch (error) {
                    console.error('Error submitting form', error);
                }
            } else {
                console.error('Form has validation errors. Please fix them.');
            }
        },
        async atualizar() {
            if (this.valid) {
                try {
                    const payload: IFeriasUpdateDTO = {
                        idFerias: this.idFerias,
                        dtInicio: this.dtInicio,
                        qtDias: this.qtDias
                    }

                    const response = await putFerias(payload);

                    //console.log(response);

                    if (response) {
                        // Sucesso, volta para a lista de items
                        this.$router.go(-1);
                    }
                    else {
                        // Erro, mostra erro
                        this.showErros = true;
                    }
                }
                catch (error) {
                    console.error('Error submitting form', error);
                }
            } else {
                console.error('Form has validation errors. Please fix them.');
            }
        },
        async confirmarDeletar() {
            this.showAlertaDeletar = true;
        },
        async deletar() {
            if (this.idFerias > 0) {
                try {
                    const response = await deleteFerias(this.idFerias);

                    //console.log(response);

                    if (response) {
                        // Sucesso, volta para a lista de items
                        this.$router.go(-1);
                    }
                    else {
                        // Erro, mostra erro
                        this.showErros = true;
                    }
                }
                catch (error) {
                    console.error('Error submitting form', error);
                }
            } else {
                console.error('Form has validation errors. Please fix them.');
            }
        }
    },
    watch: {
        idFerias(newValue) {
            this.editando = this.idFerias > 0;

            this.carregaFerias()
                .then(() => {
                    //this.loading = false;
                });
        }
    },
    mounted() {
        this.carregaFuncionarios();

        this.loading = false;
    }
}
</script> 