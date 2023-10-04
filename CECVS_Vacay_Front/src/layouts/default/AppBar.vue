<template>
  <div>
    <v-navigation-drawer v-model="isDrawerOpen">
      <Menu />
    </v-navigation-drawer>

    <v-app-bar color="teal-darken-1" density="compact">
      <template v-slot:image>
        <!-- <v-img gradient="to top right, rgba(19, 84, 122, .8), rgba(128, 208, 199, .8)"></v-img> -->
        <v-img gradient="to top right, rgba(0, 57, 186, 0), rgba(0, 57, 186, 1)"></v-img> 
        <!-- <v-img gradient="to top right, rgba(0, 57, 186, 1), rgba(255, 109, 0, 1)"></v-img> -->
      </template>

      <template v-slot:prepend>
        <v-app-bar-nav-icon variant="text" @click="isDrawerOpen = !isDrawerOpen">
        </v-app-bar-nav-icon>
      </template>

      <v-app-bar-title>CECVS - Vacay</v-app-bar-title>

      <v-spacer></v-spacer>

      <!-- <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>

      <template v-slot:append>
        <v-btn icon="mdi-dots-vertical"></v-btn>
      </template> -->
    </v-app-bar>
  </div>
</template> 

<script lang="ts" setup>
import { ref } from 'vue';
import Menu from '@/layouts/default/Menu.vue'

let isDrawerOpen = ref(false);
</script>