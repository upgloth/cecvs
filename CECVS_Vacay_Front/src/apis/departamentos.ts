import { apiV1 } from '@/apis'
import { IDepartamentoDTO } from '@/models/departamento';


async function getDepartamentos()
    : Promise<IDepartamentoDTO[]> {

    const { data } = await apiV1.get(
        `departamentos`
    );

    return data;
};

async function getDepartamento(idDepartamento: number)
    : Promise<IDepartamentoDTO> {
    const { data } = await apiV1.get(
        `departamentos/${idDepartamento}`
    );

    return data;
}

async function postDepartamento(departamento: IDepartamentoDTO)
    : Promise<boolean> {

    const response = await apiV1.post(
        `departamentos`, departamento
    );

    return response?.status == 201;
}

async function putDepartamento(departamento: IDepartamentoDTO)
    : Promise<boolean> {

    const response = await apiV1.put(
        `departamentos/${departamento.idDepartamento}`, departamento
    );

    return response?.status == 200;
}

async function deleteDepartamento(idDepartamento: number)
    : Promise<boolean> {

    const response = await apiV1.delete(
        `departamentos/${idDepartamento}`
    );

    return response?.status == 204;
}

export {
    getDepartamento,
    getDepartamentos,
    postDepartamento,
    putDepartamento,
    deleteDepartamento
};
