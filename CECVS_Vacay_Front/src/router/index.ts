// Composables
import { createRouter, createWebHistory } from 'vue-router'
import { HOME, DEPARTAMENTO_INDEX, DEPARTAMENTO_EDIT, FUNCIONARIO_INDEX, FUNCIONARIO_EDIT, FERIAS_INDEX, FERIAS_EDIT, RELATORIO_UNIDADE, RELATORIO_DEPARTAMENTO, DEPARTAMENTO_CREATE, FUNCIONARIO_CREATE, FERIAS_CREATE, RELATORIO_FUNCIONARIO } from '@/router/router-types'

const routes = [
  {
    path: '',
    name: HOME,
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: '',
        name: "HOME",
        component: () => import('@/views/Home.vue'),
      }
    ]
  },
  {
    path: '/departamentos',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: '',
        name: DEPARTAMENTO_INDEX,
        component: () => import('@/views/departamentos/Index.vue'),
      },
      {
        path: 'create',
        name: DEPARTAMENTO_CREATE,
        component: () => import('@/views/departamentos/Edit.vue'),
      },
      {
        path: ':id',
        name: DEPARTAMENTO_EDIT,
        component: () => import('@/views/departamentos/Edit.vue'),
      }
    ],
  },
  {
    path: '/funcionarios',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: '',
        name: FUNCIONARIO_INDEX,
        component: () => import('@/views/funcionarios/Index.vue'),
      },
      {
        path: 'create',
        name: FUNCIONARIO_CREATE,
        component: () => import('@/views/funcionarios/Edit.vue'),
      },
      {
        path: ':id',
        name: FUNCIONARIO_EDIT,
        component: () => import('@/views/funcionarios/Edit.vue'),
      },
      // {
      //   path: ':id/ferias',
      //   name: RELATORIO_FUNCIONARIO,
      //   component: () => import('@/views/funcionarios/RelatorioFerias.vue'),
      // }
    ],
  },
  {
    path: '/ferias',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: '',
        name: FERIAS_INDEX,
        component: () => import('@/views/ferias/Index.vue'),
      },
      {
        path: 'create',
        name: FERIAS_CREATE,
        component: () => import('@/views/ferias/Edit.vue'),
      },
      {
        path: ':id',
        name: FERIAS_EDIT,
        component: () => import('@/views/ferias/Edit.vue'),
      }
    ],
  }, {
    path: '/relatorios',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: 'unidade',
        name: RELATORIO_UNIDADE,
        component: () => import('@/views/relatorios/PorUnidade.vue'),
      },
      {
        path: 'departamento/:id?',
        name: RELATORIO_DEPARTAMENTO,
        component: () => import('@/views/relatorios/PorDepartamento.vue'),
      },

    ],
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
