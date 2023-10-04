<template>
    <v-list>
        <v-list-subheader>Menu</v-list-subheader>

        <!-- <v-list-item v-for="item in items" :key="item.value" :title="item.title" :value="item.value"
            :prepend-icon="item.icon" :href="item.route">
        </v-list-item> -->

        <v-list-item :key="inicio.value" :title="inicio.title" :value="inicio.value" :prepend-icon="inicio.icon"
            :href="inicio.route">
        </v-list-item>



        <v-list>
            <v-list-subheader>Cadastros</v-list-subheader>

            <v-list-item title="Departamento" value="departamento" prepend-icon="mdi-domain"
                href="/departamentos">
            </v-list-item>
            <v-list-item  title="Funcionário" value="funcionario" prepend-icon="mdi-account-multiple"
                href="/funcionarios">
            </v-list-item>
            <v-list-item  title="Férias" value="ferias" prepend-icon="mdi-umbrella-beach"
                href="/ferias">
            </v-list-item>

        </v-list>

        <v-list>
            <v-list-subheader>Relátorios</v-list-subheader>

            <v-list-item title="Unidade" value="unidade" prepend-icon="mdi-home-analytics"
                href="/relatorios/unidade">
            </v-list-item>
            <v-list-item  title="Departamento " value="departamento" prepend-icon="mdi-chart-box-outline"
                href="/relatorios/departamento">
            </v-list-item>

            <!-- <v-list-item  title="Funcionário " value="funcionario" prepend-icon="mdi-chart-bubble"
                href="/relatorios/funcionario">
            </v-list-item> -->

        </v-list>


    </v-list>
</template>

<script lang="ts">

export default {
    data() {
        return {
            inicio: { title: "Início", value: "inicio", route: "/", icon: "mdi-home" },
            items: [
                { title: "Início", value: "inicio", route: "/", icon: "mdi-home" },
                { title: "Desempenho", value: "desempenho", route: "/realizado", icon: "mdi-access-point-tick" },
                { title: "Permissões", value: "permissoes", route: "/", icon: "mdi-account-multiple" },
                { title: "Admin", value: "admin", route: "/", icon: "mdi-gavel" },
            ]
        }
    }
}
</script>