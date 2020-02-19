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
        v-on:click="selectPatient(idP, info.patientId)"
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
          v-on:click="selectCons(idC, idP, cons.consultationId)"
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
    <p v-if="!isSelected" class="not-selected-text">Выберите пациента для редактирования консультаций</p>
    <form v-if="selectedConsId !== null" action="">
      <section class="select-cons">
        <div class="cons-date">
          <p>Дата приема</p>
          <input type="date" v-model="postBody.date">
          <img src="../icons/calendar-im.png" alt="календарь">
        </div>
        <div class="cons-time">
          <p>Время приема</p>
          <input type="time" v-model="postBody.time">
        </div>
        <div class="cons-symptoms">
          <p>Симптомы</p>
          <textarea
            v-model="postBody.symptoms"
            cols="50"
            placeholder="Введите симптомы"
          />
        </div>
      </section>
      <button
        type="button"
        class="button-submit"
        v-on:click="checkFormCons()"
      >
        Обновить
      </button>
      <p class="errors" v-if="errors.length">
        <b>Пожалуйста, исправьте указанные ошибки:</b>
        <ul>
          <li v-for="error in errors">{{ error }}</li>
        </ul>
      </p>
    </form>
  </div>
</template>

<script>
  import axios from "axios";
  import moment from "moment";

  export default {
    name: "ConsultPatients",
    data() {
      return {
        inform: null,
        errors: [],
        selectedId: null,
        isSelected: false,
        selectedConsId: null,
        consultationId: null,
        postBody: {
          "consultationId": null,
          "date": null,
          "time": null,
          "symptoms": null,
          "patientId": null,
        },
      }
    },
    methods: {
      selectPatient(idP, patientId) {
        this.selectedId = idP;
        this.isSelected = true;
        this.postBody.patientId = patientId;
      },
      convertDate(date) {
        return moment(date).format('DD.MM.YYYY');
      },
      convertTime(time) {
        return moment(time).format('HH:mm');
      },
      selectCons(idC, idP, consId) {
        this.selectedConsId = idC;
        this.consultationId = consId;
        this.postBody.consultationId = consId;
      },
      checkFormCons() {
        this.errors = [];

        if (!this.postBody.patientId) {
          this.errors.push('Выберите пациента');
        }

        if (!this.postBody.date) {
          this.errors.push('Укажите дату');
        }

        if (!this.postBody.time) {
          this.errors.push('Укажите время');
        }

        if (!this.postBody.symptoms) {
          this.errors.push('Укажите симптомы');
        }

        if (!this.errors.length) {
          this.updateData();
        }
      },
      updateData() {
        axios.put(`https://localhost:5001/api/consultation/${this.postBody.consultationId}`, this.postBody)
          .then((response) => {
            console.log(response);
            axios
              .get('https://localhost:5001/api/consultation')
              .then(response => (this.inform = response.data));

              this.selectedId = null;
              this.isSelected = false;
              this.selectedConsId = null;
              this.consultationId = null;
              this.postBody = {
                "consultationId": null,
                "date": null,
                "time": null,
                "symptoms": null,
                "patientId": null,
            }
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

</style>