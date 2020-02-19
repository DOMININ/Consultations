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
        <div class="inform-block"
             v-bind:class="{selected: selectedId === idP}">
          <p class="inform-element">{{ info.firstName }}</p>
          <p class="inform-element">{{ info.lastName }}</p>
          <p class="inform-element">{{ info.middleName }}</p>
          <p class="inform-element">{{ info.snils }}</p>
        </div>
      </li>
    </ul>
    <form action="">
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
        @click="checkFormCons"
      >
        Добавить
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

  export default {
    name: "CreateConsultations",
    data() {
      return {
        errors: [],
        inform: null,
        selectedId: null,
        postBody: {
          "date": null,
          "time": null,
          "symptoms": null,
          "patientId": null,
        },
      }
    },
    methods: {
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
          this.postBody.time = `${this.postBody.date}T${this.postBody.time}:00`;
          this.sendData();
        }
      },
      sendData() {
        axios.post('https://localhost:5001/api/consultation', this.postBody)
          .then((response) => {
            console.log(response);
            this.selectedId = null;
            this.postBody = {
              "date": null,
              "time": null,
              "symptoms": null,
              "patientId": null,
            }
          })
          .catch((error) => {
            console.log(error);
          });
      },
      selectPatient(id, patientId) {
        this.selectedId = id;
        this.postBody.patientId = patientId;
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
  .select-cons {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    text-align: center;
    margin-top: 20px;
  }

  .cons-date {
    position: relative;
    display: flex;
    justify-content: space-between;
    min-width: 320px;

    & p {
      margin: 0;
      padding-right: 50px;
    }
  }

  .cons-time {
    display: flex;
    justify-content: space-between;
    min-width: 320px;

    & p {
      margin: 0;
      padding-left: 70px;
    }
  }

  .cons-symptoms {
    & p {
      margin: 15px 0 0;
    }
  }

  input[type="time"] {
    padding-left: 15px;
    font-size: 18px;
    line-height: 20px;
    border: 1px solid #868686;
    border-radius: 50px;
  }

  textarea {
    resize: none;
    padding: 15px;
    font-size: 16px;
    border: 1px solid #868686;
    border-radius: 50px;
  }
</style>