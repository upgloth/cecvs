<template>
    <h1>Funcionário</h1>

    <v-alert v-if="showAlertaDeletar" type="warning" title="Confirmação Necessária" border="start"
        text="Ao deletar um funcionário todos os dados relativos as férias serão perdidos, continuar?">
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

            <v-text-field v-model="coMatricula" type="text" :rules="coMatriculaRules" label="Matrícula" />

            <v-text-field v-model="noFuncionario" type="text" :rules="noFuncionarioRules" label="Nome" />

            <v-text-field v-model="dtAdmissao" type="date" :rules="dtAdmissaoRules" label="Admissão" />

            <v-select v-model="idDepartamento" :items="$store.state.departamentos" item-title="noDepartamento"
                item-value="idDepartamento" label="Departamento" :rules="idDepartamentoRules">
            </v-select>

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
import { deleteFuncionario, getFuncionario, postFuncionario, putFuncionario } from '@/apis/funcionarios';
import { IFuncionarioDTO } from '@/models/funcionario';

export default {
    props: {
        idFuncionario: null,
    },
    data() {
        return {
            valid: false,
            coMatricula: '',
            noFuncionario: '',
            dtAdmissao: '',
            idDepartamento: null,
            editando: false,
            loading: true,
            showAlertaDeletar: false,
            showErros: false,
            errorText: 'Erro ao executar operação (Nesta versão o erro pode ser verificado no console).',
            coMatriculaRules: [
                (value: string) => {
                    const regex = new RegExp(/^[Cc]\d{6}$/);

                    if (regex.test(value)) return true;

                    return "Matrícula deve estar no formato CXXXXXX (ex: C123123|c123123).";
                }
            ],
            noFuncionarioRules: [
                (value: string) => {
                    if (value && value.length >= 3 && value.length <= 50) return true;

                    return 'Nome do funcionário é obrigatório e deve ter no minímo 3 e no maxímo 50 caracteres.';
                }
            ],
            idDepartamentoRules: [
                (value: number) => {
                    if (value) return true;

                    return 'Departamento é obrigatório.';
                }
            ],
            dtAdmissaoRules: [
                (value: string) => {
                    if (value && value.length >= 10) return true;

                    return 'Data admissão inválida.';
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
        async carregaFuncionario() {
            // Carrega funcionario 
            const funcionario = await getFuncionario(this.idFuncionario);

            this.coMatricula = funcionario.coMatricula;
            this.noFuncionario = funcionario.noFuncionario;
            this.dtAdmissao = funcionario.dtAdmissao.substring(0, 10);
            this.idDepartamento = funcionario.idDepartamento;
        },
        async salvar() {
            if (this.valid) {
                try {
                    const payload: IFuncionarioDTO = {
                        idFuncionario: 0,
                        coMatricula: this.coMatricula,
                        noFuncionario: this.noFuncionario,
                        dtAdmissao: this.dtAdmissao,
                        idDepartamento: this.idDepartamento
                    }

                    const response = await postFuncionario(payload);

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
                    const payload: IFuncionarioDTO = {
                        idFuncionario: this.idFuncionario,
                        coMatricula: this.coMatricula,
                        noFuncionario: this.noFuncionario,
                        dtAdmissao: this.dtAdmissao,
                        idDepartamento: this.idDepartamento
                    }

                    const response = await putFuncionario(payload);

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
            if (this.idFuncionario > 0) {
                try {
                    const response = await deleteFuncionario(this.idFuncionario);

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
        idFuncionario(newValue) {
            this.editando = this.idFuncionario > 0;

            this.carregaFuncionario()
                .then(() => {
                    //this.loading = false;
                });
        }
    },
    mounted() {
        this.loading = false;
    }
}
</script> 