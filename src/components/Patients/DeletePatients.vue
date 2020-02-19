<template>
  <div>
    <table class="all">
      <thead>
        <tr>
          <th>Фамилия</th>
          <th>Имя</th>
          <th>Отчество</th>
          <th>Дата рождения</th>
          <th>Пол</th>
          <th>СНИЛС</th>
        </tr>
      </thead>
      <tbody>
        <tr
          class="patient-row row"
          v-for="(info, id) in this.inform" :key="id"
          v-on:click="selectPatient(id, info.patientId)"
          v-bind:class="{selected: selectedId === id}"
        >
          <td class="all-info-item">{{ info.firstName }}</td>
          <td class="all-info-item">{{ info.lastName }}</td>
          <td class="all-info-item">{{ info.middleName }}</td>
          <td class="all-info-item">{{ convertDate(info.birthDate) }}</td>
          <td class="all-info-item">{{ info.gender }}</td>
          <td class="all-info-item">{{ info.snils }}</td>
        </tr>
      </tbody>
    </table>
    <button
      type="button"
      class="button-submit"
      v-if="isSelected"
      @click="deleteData()"
    >
      Удалить пациента
    </button>
    <p class="not-selected-text" v-else>Выберите пациента, которого нужно удалить</p>
  </div>
</template>

<script>
  import axios from "axios";
  import moment from 'moment';

  export default {
    name: "DeletePatients",
    data() {
      return {
        inform: null,
        isSelected: false,
        selectedId: null,
        patientId: null,
      }
    },
    methods: {
      convertDate(date) {
        return moment(date).format('DD.MM.YYYY');
      },
      selectPatient(id, patientId) {
        this.selectedId = id;
        this.isSelected = true;

        this.patientId = patientId;
      },
      deleteData() {
        axios.delete(`https://localhost:5001/api/patient/${this.patientId}`)
          .then((response) => {
            console.log(response);
            axios
              .get('https://localhost:5001/api/patient')
              .then(response => (this.inform = response.data));
            this.isSelected = false;
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    mounted() {
      axios
        .get('https://localhost:5001/api/patient')
        .then(response => (this.inform = response.data));
    },
  }
</script>

<style lang="scss">

</style>