import { SET_DEPARTAMENTOS } from '@/store/mutation-types'
import { IDepartamentoDTO } from '@/models/departamento';

export default ({
    [SET_DEPARTAMENTOS](state, data: IDepartamentoDTO[]) {
        state.departamentos = data;
    },
});