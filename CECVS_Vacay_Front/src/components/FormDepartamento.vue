<template>
    <h1>Departamento</h1>

    <v-alert v-if="showAlertaDeletar" type="warning" title="Confirmação Necessária" border="start"
        text="Ao deletar um departamento todos os dados do departamento e seus funcionários serão perdidos, continuar?">
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

            <v-text-field v-model="noDepartamento" type="text" :rules="noDepartamentoRules" label="Nome Departamento" />

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
import { getEnvironmentData } from 'worker_threads';
import { ref } from "vue";
import { deleteDepartamento, getDepartamento, postDepartamento, putDepartamento } from '@/apis/departamentos';
import { IDepartamentoDTO } from '@/models/departamento';

export default {
    props: {
        idDepartamento: null,
    },
    data() {
        return {
            valid: false,
            noDepartamento: "",
            editando: false,
            loading: true,
            showAlertaDeletar: false,
            showErros: false,
            errorText: 'Erro ao executar operação (Nesta versão o erro pode ser verificado no console).',
            noDepartamentoRules: [
                (value: string) => {
                    if (value && value.length >= 3 && value.length <= 50) return true;

                    return 'Nome do Departamento é obrigatório e deve ter no minímo 3 e no maxímo 50 caracteres.';
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
        async carregaDepartamento() {
            // Carrega departamento
            const departamento = await getDepartamento(this.idDepartamento);

            this.noDepartamento = departamento.noDepartamento;
        },
        async salvar() {
            if (this.valid) {
                try {
                    const payload: IDepartamentoDTO = {
                        idDepartamento: 0,
                        noDepartamento: this.noDepartamento
                    }

                    const response = await postDepartamento(payload);

                    //console.log(response);

                    if (response) {
                        // Sucesso, volta para a lista de items
                        this.$router.go(-1);
                    }
                    else {
                        // Erro, mostra erro
                        this.errorText = "Erro ao salvar, certifique-se de que os dados informados são válidos e tente novamente.";
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
                    const payload: IDepartamentoDTO = {
                        idDepartamento: this.idDepartamento,
                        noDepartamento: this.noDepartamento
                    }

                    const response = await putDepartamento(payload);

                    //console.log(response);

                    if (response) {
                        // Sucesso, volta para a lista de items
                        this.$router.go(-1);
                    }
                    else {
                        // Erro, mostra erro
                        this.errorText = "Erro ao atualizar, certifique-se de que os dados informados são válidos e tente novamente.";
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
            if (this.idDepartamento > 0) {
                try {
                    const response = await deleteDepartamento(this.idDepartamento);

                    //console.log(response);

                    if (response) {
                        // Sucesso, volta para a lista de items
                        this.$router.go(-1);
                    }
                    else {
                        // Erro, mostra erro
                        this.errorText = "Erro ao deletar, tente novamente.";
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
        idDepartamento(newValue) {
            this.editando = this.idDepartamento > 0;

            this.carregaDepartamento()
                .then(() => {
                    //this.loading = false;
                });
        }
    },
    mounted() {
        this.loading = false;
    },
}
</script> 