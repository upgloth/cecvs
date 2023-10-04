import { apiV1 } from '@/apis'
import { IFeriasDTO, IFeriasExpandidoDTO, IFeriasUpdateDTO } from '@/models/ferias';

async function getFerias(idFerias: number)
    : Promise<IFeriasDTO> {
    const { data } = await apiV1.get(
        `ferias/${idFerias}`
    );

    return data;
}

async function getFeriasList()
    : Promise<IFeriasExpandidoDTO[]> {

    const { data } = await apiV1.get(
        `relatorios/ferias`
    );

    return data;
};

async function getFeriasByIdDepartamento(idDepartamento: number)
    : Promise<IFeriasExpandidoDTO[]> {

    const { data } = await apiV1.get(
        `relatorios/ferias/departamento/${idDepartamento}`
    );

    return data;
};

async function postFerias(ferias: IFeriasDTO)
    : Promise<boolean> {

    const response = await apiV1.post(
        `ferias`, ferias
    );

    return response?.status == 201;
}

async function putFerias(ferias: IFeriasUpdateDTO)
    : Promise<boolean> {

    const response = await apiV1.put(
        `ferias/${ferias.idFerias}`, ferias
    );

    return response?.status == 200;
}

async function deleteFerias(idFerias: number)
    : Promise<boolean> {

    const response = await apiV1.delete(
        `ferias/${idFerias}`
    );

    return response?.status == 204;
}

export {
    getFerias,
    getFeriasList,
    getFeriasByIdDepartamento,
    postFerias,
    putFerias,
    deleteFerias
};