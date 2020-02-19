<template>
  <div>
    <div class="title-block">
      <p class="title-element">Фамилия</p>
      <p class="title-element">Имя</p>
      <p class="title-element">Отчество</p>
      <p class="title-element">СНИЛС</p>
    </div>
    <ul class="inform-list">
      <li
        class="inform-item"
        v-for="(info, idP) in this.inform"
        :key="idP"
        v-on:click="selectPatient(idP)"
      >
        <div class="inform-block">
          <p class="inform-element">{{ info.firstName }}</p>
          <p class="inform-element">{{ info.lastName }}</p>
          <p class="inform-element">{{ info.middleName }}</p>
          <p class="inform-element">{{ info.snils }}</p>
        </div>
        <ul
          v-if="selectedId === idP"
          v-for="(cons, idC) in info.consultations"
          :key="idC"
          class="cons-list search-cons"
          v-on:click="selectCons(idC, cons.consultationId)"
          v-bind:class="{selected: selectedConsId === idC && selectedId === idP}"
        >
          <li class="cons-item">{{ idC + 1 }}</li>
          <li class="cons-item">Дата: {{ convertDate(cons.date) }}</li>
          <li class="cons-item">Время: {{ convertTime(cons.time) }}</li>
          <li class="cons-item">Симптомы: <br> {{ cons.symptoms }}</li>
        </ul>
        <p
          v-if="selectedId === idP && Object.keys(info.consultations).length === 0"
          class="cons-empty"
        >У данного пациента нет консультаций</p>
      </li>
    </ul>
    <p v-if="!isSelected" class="not-selected-text">Выберите пациента для удаления консультации</p>
    <button
      type="button"
      class="button-submit"
      v-if="selectedConsId !== null"
      @click="deleteData()"
    >
      Удалить запись
    </button>
  </div>
</template>

<script>
  import axios from "axios";
  import moment from "moment";

  export default {
    name: "DeleteConsultations",
    data() {
      return {
        inform: null,
        selectedId: null,
        isSelected: false,
        selectedConsId: null,
        consultationId: null,
      }
    },
    methods: {
      selectPatient(idP) {
        this.selectedId = idP;
        this.isSelected = true;
      },
      selectCons(idC, consId) {
        this.selectedConsId = idC;
        this.consultationId = consId;
      },
      convertDate(date) {
        return moment(date).format('DD.MM.YYYY');
      },
      convertTime(time) {
        return moment(time).format('HH:mm');
      },
      deleteData() {
        axios.delete(`https://localhost:5001/api/consultation/${this.consultationId}`)
          .then((response) => {
            console.log(response);
            axios
              .get('https://localhost:5001/api/consultation')
              .then(response => (this.inform = response.data));

            this.selectedId = null;
            this.isSelected = false;
            this.selectedConsId = null;
            this.consultationId = null;
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    mounted() {
      axios
        .get('https://localhost:5001/api/consultation')
        .then(response => (this.inform = response.data));
    },
  }
</script>

<style lang="scss">
  .search-cons {
    cursor: pointer;

    &:hover {
      background-color: #ed462f;
    }
  }
</style>